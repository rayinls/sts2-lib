using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Ascension;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000496 RID: 1174
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class EncounterModel : AbstractModel
	{
		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x00200B46 File Offset: 0x001FED46
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600473B RID: 18235 RVA: 0x00200B49 File Offset: 0x001FED49
		protected Rng Rng
		{
			get
			{
				return this._rng;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x0600473C RID: 18236
		public abstract RoomType RoomType { get; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x0600473D RID: 18237 RVA: 0x00200B51 File Offset: 0x001FED51
		public virtual bool IsWeak
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600473E RID: 18238 RVA: 0x00200B54 File Offset: 0x001FED54
		public virtual bool ShouldGiveRewards
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x00200B58 File Offset: 0x001FED58
		public virtual int MinGoldReward
		{
			get
			{
				int num;
				switch (this.RoomType)
				{
				case RoomType.Monster:
					num = 10;
					break;
				case RoomType.Elite:
					num = 35;
					break;
				case RoomType.Boss:
					num = 100;
					break;
				default:
					num = 0;
					break;
				}
				double num2 = (double)num;
				if (AscensionHelper.HasAscension(AscensionLevel.Poverty))
				{
					num2 *= AscensionHelper.PovertyAscensionGoldMultiplier;
				}
				return (int)num2;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x00200BA8 File Offset: 0x001FEDA8
		public virtual int MaxGoldReward
		{
			get
			{
				int num;
				switch (this.RoomType)
				{
				case RoomType.Monster:
					num = 20;
					break;
				case RoomType.Elite:
					num = 45;
					break;
				case RoomType.Boss:
					num = 100;
					break;
				default:
					num = 0;
					break;
				}
				double num2 = (double)num;
				if (AscensionHelper.HasAscension(AscensionLevel.Poverty))
				{
					num2 *= AscensionHelper.PovertyAscensionGoldMultiplier;
				}
				return (int)num2;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x00200BF8 File Offset: 0x001FEDF8
		[Nullable(2)]
		public LocString CustomRewardDescription
		{
			[NullableContext(2)]
			get
			{
				return LocString.GetIfExists("encounters", base.Id.Entry + ".customRewardDescription");
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x00200C19 File Offset: 0x001FEE19
		public virtual bool IsDebugEncounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x00200C1C File Offset: 0x001FEE1C
		public virtual IEnumerable<EncounterTag> Tags
		{
			get
			{
				return Array.Empty<EncounterTag>();
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x00200C23 File Offset: 0x001FEE23
		public bool HaveMonstersBeenGenerated
		{
			get
			{
				return this._monstersWithSlots != null;
			}
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x00200C2E File Offset: 0x001FEE2E
		public virtual float GetCameraScaling()
		{
			return 1f;
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00200C35 File Offset: 0x001FEE35
		public virtual Vector2 GetCameraOffset()
		{
			return Vector2.Zero;
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x00200C3C File Offset: 0x001FEE3C
		public string GetNextSlot(CombatState combatState)
		{
			return this.Slots.FirstOrDefault((string s) => combatState.Enemies.All((Creature c) => c.SlotName != s), string.Empty);
		}

		// Token: 0x06004748 RID: 18248
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected abstract IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters();

		// Token: 0x06004749 RID: 18249 RVA: 0x00200C74 File Offset: 0x001FEE74
		public void GenerateMonstersWithSlots(IRunState runState)
		{
			base.AssertMutable();
			if (this._monstersWithSlots != null)
			{
				throw new InvalidOperationException("Monsters have already been generated for this encounter.");
			}
			if (this._rng == null)
			{
				uint num = runState.Rng.Seed + (uint)runState.TotalFloor + (uint)StringHelper.GetDeterministicHashCode(base.Id.Entry);
				this._rng = new Rng(num, 0);
			}
			this._monstersWithSlots = this.GenerateMonsters();
			foreach (ValueTuple<MonsterModel, string> valueTuple in this._monstersWithSlots)
			{
				MonsterModel item = valueTuple.Item1;
				item.AssertMutable();
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x00200D24 File Offset: 0x001FEF24
		[Nullable(new byte[] { 1, 0, 1, 2 })]
		public IReadOnlyList<ValueTuple<MonsterModel, string>> MonstersWithSlots
		{
			[return: Nullable(new byte[] { 1, 0, 1, 2 })]
			get
			{
				base.AssertMutable();
				if (this._monstersWithSlots == null)
				{
					throw new InvalidOperationException("GenerateMonstersWithSlots must be called before using this property!");
				}
				return this._monstersWithSlots;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x0600474B RID: 18251
		public abstract IEnumerable<MonsterModel> AllPossibleMonsters { get; }

		// Token: 0x0600474C RID: 18252 RVA: 0x00200D45 File Offset: 0x001FEF45
		[NullableContext(2)]
		public bool SharesTagsWith(EncounterModel other)
		{
			return other != null && this.Tags.Intersect(other.Tags).Any<EncounterTag>();
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600474D RID: 18253 RVA: 0x00200D62 File Offset: 0x001FEF62
		// (set) Token: 0x0600474E RID: 18254 RVA: 0x00200D74 File Offset: 0x001FEF74
		public EncounterModel CanonicalInstance
		{
			get
			{
				if (!base.IsMutable)
				{
					return this;
				}
				return this._canonicalInstance;
			}
			private set
			{
				base.AssertMutable();
				this._canonicalInstance = value;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600474F RID: 18255 RVA: 0x00200D83 File Offset: 0x001FEF83
		public virtual bool HasScene
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x00200D86 File Offset: 0x001FEF86
		public virtual IReadOnlyList<string> Slots
		{
			get
			{
				return Array.Empty<string>();
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004751 RID: 18257 RVA: 0x00200D8D File Offset: 0x001FEF8D
		public virtual bool FullyCenterPlayers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004752 RID: 18258 RVA: 0x00200D90 File Offset: 0x001FEF90
		private string ScenePath
		{
			get
			{
				return SceneHelper.GetScenePath("encounters/" + base.Id.Entry.ToLowerInvariant());
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004753 RID: 18259 RVA: 0x00200DB1 File Offset: 0x001FEFB1
		protected virtual bool HasCustomBackground
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00200DB4 File Offset: 0x001FEFB4
		public NCombatBackground CreateBackground(ActModel parentAct, Rng rng)
		{
			return NCombatBackground.Create(this.GetBackgroundAssets(parentAct, rng));
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x00200DC3 File Offset: 0x001FEFC3
		private BackgroundAssets GetBackgroundAssets(ActModel parentAct, Rng rng)
		{
			base.AssertMutable();
			if (this._backgroundAssets == null)
			{
				if (this.HasCustomBackground)
				{
					this._backgroundAssets = this.CreateBackgroundAssetsForCustom(rng);
				}
				else
				{
					this._backgroundAssets = parentAct.GenerateBackgroundAssets(rng);
				}
			}
			return this._backgroundAssets;
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x00200DFD File Offset: 0x001FEFFD
		private BackgroundAssets CreateBackgroundAssetsForCustom(Rng rng)
		{
			return new BackgroundAssets(base.Id.Entry.ToLowerInvariant(), rng);
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06004757 RID: 18263 RVA: 0x00200E15 File Offset: 0x001FF015
		public virtual string CustomBgm
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x00200E1C File Offset: 0x001FF01C
		public bool HasBgm
		{
			get
			{
				return this.CustomBgm != "";
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004759 RID: 18265 RVA: 0x00200E2E File Offset: 0x001FF02E
		public virtual string AmbientSfx
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x00200E35 File Offset: 0x001FF035
		public bool HasAmbientSfx
		{
			get
			{
				return this.AmbientSfx != "";
			}
		}

		// Token: 0x0600475B RID: 18267 RVA: 0x00200E47 File Offset: 0x001FF047
		public Control CreateScene()
		{
			return PreloadManager.Cache.GetScene(this.ScenePath).Instantiate<Control>(PackedScene.GenEditState.Disabled);
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600475C RID: 18268 RVA: 0x00200E60 File Offset: 0x001FF060
		public virtual string BossNodePath
		{
			get
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(42, 2);
				defaultInterpolatedStringHandler.AppendLiteral("res://animations/map/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("/");
				defaultInterpolatedStringHandler.AppendFormatted(base.Id.Entry.ToLowerInvariant());
				defaultInterpolatedStringHandler.AppendLiteral("_node_skel_data.tres");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600475D RID: 18269 RVA: 0x00200ED0 File Offset: 0x001FF0D0
		[Nullable(2)]
		public virtual MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				if (!ResourceLoader.Exists(this.BossNodePath, ""))
				{
					return null;
				}
				return new MegaSkeletonDataResource(PreloadManager.Cache.GetAsset<Resource>(this.BossNodePath));
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x00200F00 File Offset: 0x001FF100
		public LocString Title
		{
			get
			{
				return EncounterModel.L10NLookup(base.Id.Entry + ".title");
			}
		}

		// Token: 0x0600475F RID: 18271 RVA: 0x00200F1C File Offset: 0x001FF11C
		public EncounterModel ToMutable()
		{
			base.AssertCanonical();
			EncounterModel encounterModel = (EncounterModel)base.MutableClone();
			encounterModel.CanonicalInstance = this;
			return encounterModel;
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x00200F44 File Offset: 0x001FF144
		public IEnumerable<string> MapNodeAssetPaths
		{
			get
			{
				if (this.BossNodeSpineResource != null)
				{
					return new <>z__ReadOnlySingleElementList<string>(this.BossNodePath);
				}
				return new <>z__ReadOnlyArray<string>(new string[]
				{
					this.BossNodePath + ".png",
					this.BossNodePath + "_outline.png"
				});
			}
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x00200F98 File Offset: 0x001FF198
		public IEnumerable<string> GetAssetPaths(IRunState runState)
		{
			HashSet<string> hashSet = new HashSet<string>();
			hashSet.UnionWith(this.GetBackgroundAssets(runState.Act, NCombatRoom.GenerateBackgroundRngForCurrentPoint(runState)).AssetPaths);
			if (this.HasScene)
			{
				hashSet.Add(this.ScenePath);
			}
			if (this.ExtraAssetPaths != null)
			{
				hashSet.UnionWith(this.ExtraAssetPaths);
			}
			foreach (ValueTuple<MonsterModel, string> valueTuple in this.MonstersWithSlots)
			{
				MonsterModel item = valueTuple.Item1;
				hashSet.UnionWith(item.AssetPaths);
			}
			return hashSet;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x0020103C File Offset: 0x001FF23C
		[Nullable(new byte[] { 2, 1 })]
		public virtual IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return null;
			}
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x00201040 File Offset: 0x001FF240
		public void DebugRandomizeRng()
		{
			base.AssertMutable();
			this._rng = new Rng((uint)(DateTime.UtcNow - DateTime.UnixEpoch).TotalSeconds, 0);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x00201078 File Offset: 0x001FF278
		public LocString GetLossMessageFor(CharacterModel character)
		{
			LocString locString = EncounterModel.L10NLookup(base.Id.Entry + ".loss");
			character.AddDetailsTo(locString);
			locString.Add("encounter", this.Title);
			return locString;
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x002010B9 File Offset: 0x001FF2B9
		public virtual Dictionary<string, string> SaveCustomState()
		{
			return new Dictionary<string, string>();
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x002010C0 File Offset: 0x001FF2C0
		public virtual void LoadCustomState(Dictionary<string, string> state)
		{
		}

		// Token: 0x06004767 RID: 18279 RVA: 0x002010C2 File Offset: 0x001FF2C2
		private static LocString L10NLookup(string key)
		{
			return new LocString("encounters", key);
		}

		// Token: 0x04001AD4 RID: 6868
		private const string _locTable = "encounters";

		// Token: 0x04001AD5 RID: 6869
		[Nullable(2)]
		private Rng _rng;

		// Token: 0x04001AD6 RID: 6870
		[Nullable(new byte[] { 2, 0, 1, 2 })]
		private IReadOnlyList<ValueTuple<MonsterModel, string>> _monstersWithSlots;

		// Token: 0x04001AD7 RID: 6871
		private EncounterModel _canonicalInstance;

		// Token: 0x04001AD8 RID: 6872
		[Nullable(2)]
		private BackgroundAssets _backgroundAssets;
	}
}
