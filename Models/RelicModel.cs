using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x020004A2 RID: 1186
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class RelicModel : AbstractModel
	{
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x00204CF4 File Offset: 0x00202EF4
		public virtual LocString Title
		{
			get
			{
				LocString locString = new LocString("relics", base.Id.Entry + ".title");
				if (this.IsWax)
				{
					LocString waxRelicPrefix = ToyBox.WaxRelicPrefix;
					waxRelicPrefix.Add("Title", locString);
					locString = waxRelicPrefix;
				}
				return locString;
			}
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x00204D3E File Offset: 0x00202F3E
		public LocString Flavor
		{
			get
			{
				return new LocString("relics", base.Id.Entry + ".flavor");
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x00204D5F File Offset: 0x00202F5F
		protected LocString EventDescription
		{
			get
			{
				return LocString.GetIfExists("relics", base.Id.Entry + ".eventDescription") ?? this.Description;
			}
		}

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x00204D8A File Offset: 0x00202F8A
		public LocString Description
		{
			get
			{
				return new LocString("relics", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600491D RID: 18717 RVA: 0x00204DAC File Offset: 0x00202FAC
		protected LocString SelectionScreenPrompt
		{
			get
			{
				LocString locString = new LocString("relics", base.Id.Entry + ".selectionScreenPrompt");
				this.DynamicVars.AddTo(locString);
				return locString;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x00204DE8 File Offset: 0x00202FE8
		public LocString DynamicEventDescription
		{
			get
			{
				LocString eventDescription = this.EventDescription;
				this.DynamicVars.AddTo(eventDescription);
				eventDescription.Add("energyPrefix", EnergyIconHelper.GetPrefix(this));
				eventDescription.Add("singleStarIcon", "[img]res://images/packed/sprite_fonts/star_icon.png[/img]");
				return eventDescription;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x0600491F RID: 18719 RVA: 0x00204E2C File Offset: 0x0020302C
		public LocString DynamicDescription
		{
			get
			{
				LocString description = this.Description;
				this.DynamicVars.AddTo(description);
				string prefix = EnergyIconHelper.GetPrefix(this);
				description.Add("energyPrefix", prefix);
				description.Add("singleStarIcon", "[img]res://images/packed/sprite_fonts/star_icon.png[/img]");
				foreach (KeyValuePair<string, object> keyValuePair in description.Variables)
				{
					EnergyVar energyVar = keyValuePair.Value as EnergyVar;
					if (energyVar != null)
					{
						energyVar.ColorPrefix = prefix;
					}
				}
				return description;
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x00204EC4 File Offset: 0x002030C4
		[Nullable(2)]
		protected LocString AdditionalRestSiteHealText
		{
			[NullableContext(2)]
			get
			{
				LocString ifExists = LocString.GetIfExists("relics", base.Id.Entry + ".additionalRestSiteHealText");
				if (ifExists != null)
				{
					this.DynamicVars.AddTo(ifExists);
				}
				return ifExists;
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004921 RID: 18721 RVA: 0x00204F01 File Offset: 0x00203101
		protected virtual string IconBaseName
		{
			get
			{
				return base.Id.Entry.ToLowerInvariant();
			}
		}

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004922 RID: 18722 RVA: 0x00204F13 File Offset: 0x00203113
		public virtual string PackedIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("atlases/relic_atlas.sprites/" + this.IconBaseName + ".tres");
			}
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004923 RID: 18723 RVA: 0x00204F2F File Offset: 0x0020312F
		protected virtual string PackedIconOutlinePath
		{
			get
			{
				return ImageHelper.GetImagePath("atlases/relic_outline_atlas.sprites/" + this.IconBaseName + ".tres");
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004924 RID: 18724 RVA: 0x00204F4B File Offset: 0x0020314B
		protected virtual string BigIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("relics/" + this.IconBaseName + ".png");
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x00204F67 File Offset: 0x00203167
		private string BigBetaIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("relics/beta/" + this.IconBaseName + ".png");
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004926 RID: 18726 RVA: 0x00204F83 File Offset: 0x00203183
		private static string MissingIconPath
		{
			get
			{
				return ImageHelper.GetImagePath("powers/missing_power.png");
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x00204F8F File Offset: 0x0020318F
		public string IconPath
		{
			get
			{
				return this.PackedIconPath;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004928 RID: 18728 RVA: 0x00204F97 File Offset: 0x00203197
		public Texture2D Icon
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.PackedIconPath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x00204FA7 File Offset: 0x002031A7
		public Texture2D IconOutline
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.PackedIconOutlinePath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600492A RID: 18730 RVA: 0x00204FB7 File Offset: 0x002031B7
		public Texture2D BigIcon
		{
			get
			{
				return PreloadManager.Cache.GetTexture2D(this.ResolvedBigIconPath);
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x00204FCC File Offset: 0x002031CC
		private string ResolvedBigIconPath
		{
			get
			{
				if (this._resolvedBigIconPath != null)
				{
					return this._resolvedBigIconPath;
				}
				if (ResourceLoader.Exists(this.BigIconPath, ""))
				{
					this._resolvedBigIconPath = this.BigIconPath;
				}
				else if (ResourceLoader.Exists(this.BigBetaIconPath, ""))
				{
					this._resolvedBigIconPath = this.BigBetaIconPath;
				}
				else
				{
					this._resolvedBigIconPath = RelicModel.MissingIconPath;
				}
				return this._resolvedBigIconPath;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600492C RID: 18732
		public abstract RelicRarity Rarity { get; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x00205039 File Offset: 0x00203239
		public RelicPoolModel Pool
		{
			get
			{
				return ModelDb.AllRelicPools.First((RelicPoolModel p) => p.AllRelicIds.Contains(base.Id));
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600492E RID: 18734 RVA: 0x00205054 File Offset: 0x00203254
		public bool IsTradable
		{
			get
			{
				if (this.IsUsedUp)
				{
					return false;
				}
				if (this.HasUponPickupEffect)
				{
					return false;
				}
				if (this.IsMelted)
				{
					return false;
				}
				if (this.SpawnsPets)
				{
					return false;
				}
				RelicRarity rarity = this.Rarity;
				bool flag = rarity == RelicRarity.Starter || rarity - RelicRarity.Event <= 1;
				return !flag;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x002050A6 File Offset: 0x002032A6
		// (set) Token: 0x06004930 RID: 18736 RVA: 0x002050B4 File Offset: 0x002032B4
		public Player Owner
		{
			get
			{
				base.AssertMutable();
				return this._owner;
			}
			set
			{
				base.AssertMutable();
				if (this._owner != null && this._owner != value)
				{
					throw new InvalidOperationException("Cannot move relic from " + base.Id.Entry + " one owner to another");
				}
				this._owner = value;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004931 RID: 18737 RVA: 0x002050F4 File Offset: 0x002032F4
		public virtual bool IsUsedUp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x002050F7 File Offset: 0x002032F7
		public virtual bool HasUponPickupEffect
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004933 RID: 18739 RVA: 0x002050FA File Offset: 0x002032FA
		public virtual bool SpawnsPets
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x002050FD File Offset: 0x002032FD
		public virtual bool IsStackable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06004935 RID: 18741 RVA: 0x00205100 File Offset: 0x00203300
		// (set) Token: 0x06004936 RID: 18742 RVA: 0x00205108 File Offset: 0x00203308
		[SavedProperty(SerializationCondition.SaveIfNotTypeDefault)]
		public bool IsWax
		{
			get
			{
				return this._isWax;
			}
			set
			{
				base.AssertMutable();
				this._isWax = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06004937 RID: 18743 RVA: 0x00205117 File Offset: 0x00203317
		// (set) Token: 0x06004938 RID: 18744 RVA: 0x0020511F File Offset: 0x0020331F
		[SavedProperty(SerializationCondition.SaveIfNotTypeDefault)]
		public bool IsMelted
		{
			get
			{
				return this._isMelted;
			}
			set
			{
				base.AssertMutable();
				this._isMelted = value;
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004939 RID: 18745 RVA: 0x0020512E File Offset: 0x0020332E
		public virtual bool AddsPet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x00205131 File Offset: 0x00203331
		// (set) Token: 0x0600493B RID: 18747 RVA: 0x00205139 File Offset: 0x00203339
		public int StackCount { get; private set; } = 1;

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x00205142 File Offset: 0x00203342
		public DynamicVarSet DynamicVars
		{
			get
			{
				if (this._dynamicVars != null)
				{
					return this._dynamicVars;
				}
				this._dynamicVars = new DynamicVarSet(this.CanonicalVars);
				this._dynamicVars.InitializeWithOwner(this);
				return this._dynamicVars;
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x0600493D RID: 18749 RVA: 0x00205176 File Offset: 0x00203376
		protected virtual IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x00205180 File Offset: 0x00203380
		public virtual int MerchantCost
		{
			get
			{
				int num;
				switch (this.Rarity)
				{
				case RelicRarity.None:
					num = 1;
					break;
				case RelicRarity.Starter:
					num = 999;
					break;
				case RelicRarity.Common:
					num = 200;
					break;
				case RelicRarity.Uncommon:
					num = 250;
					break;
				case RelicRarity.Rare:
					num = 300;
					break;
				case RelicRarity.Shop:
					num = 225;
					break;
				case RelicRarity.Event:
					num = 999;
					break;
				case RelicRarity.Ancient:
					num = 999;
					break;
				default:
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(36, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Relic ");
					defaultInterpolatedStringHandler.AppendFormatted<ModelId>(base.Id);
					defaultInterpolatedStringHandler.AppendLiteral(" has invalid merchant rarity ");
					defaultInterpolatedStringHandler.AppendFormatted<RelicRarity>(this.Rarity);
					defaultInterpolatedStringHandler.AppendLiteral(".");
					throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				}
				return num;
			}
		}

		// Token: 0x0600493F RID: 18751 RVA: 0x00205254 File Offset: 0x00203454
		public virtual bool IsAllowed(IRunState runState)
		{
			return true;
		}

		// Token: 0x06004940 RID: 18752 RVA: 0x00205258 File Offset: 0x00203458
		protected static bool IsBeforeAct3TreasureChest(IRunState runState)
		{
			int num = ((runState.Players.Count > 1) ? 38 : 41);
			return runState.TotalFloor < num;
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06004941 RID: 18753 RVA: 0x00205283 File Offset: 0x00203483
		// (set) Token: 0x06004942 RID: 18754 RVA: 0x0020528B File Offset: 0x0020348B
		public int FloorAddedToDeck
		{
			get
			{
				return this._floorAddedToDeck;
			}
			set
			{
				base.AssertMutable();
				this._floorAddedToDeck = value;
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06004943 RID: 18755 RVA: 0x0020529A File Offset: 0x0020349A
		// (set) Token: 0x06004944 RID: 18756 RVA: 0x002052A2 File Offset: 0x002034A2
		public RelicStatus Status
		{
			get
			{
				return this._status;
			}
			set
			{
				base.AssertMutable();
				if (this._status == value)
				{
					return;
				}
				this._status = value;
				Action statusChanged = this.StatusChanged;
				if (statusChanged == null)
				{
					return;
				}
				statusChanged();
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06004945 RID: 18757 RVA: 0x002052CB File Offset: 0x002034CB
		public virtual bool ShowCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x002052CE File Offset: 0x002034CE
		public virtual int DisplayAmount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004947 RID: 18759 RVA: 0x002052D1 File Offset: 0x002034D1
		public void Flash()
		{
			this.Flash(new <>z__ReadOnlySingleElementList<Creature>(this.Owner.Creature));
		}

		// Token: 0x06004948 RID: 18760 RVA: 0x002052E9 File Offset: 0x002034E9
		public void Flash(IEnumerable<Creature> targets)
		{
			Action<RelicModel, IEnumerable<Creature>> flashed = this.Flashed;
			if (flashed == null)
			{
				return;
			}
			flashed(this, targets);
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x002052FD File Offset: 0x002034FD
		public virtual string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_general";
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x0600494A RID: 18762 RVA: 0x00205304 File Offset: 0x00203504
		// (remove) Token: 0x0600494B RID: 18763 RVA: 0x0020533C File Offset: 0x0020353C
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		[field: Nullable(new byte[] { 2, 1, 1, 1 })]
		public event Action<RelicModel, IEnumerable<Creature>> Flashed;

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x00205371 File Offset: 0x00203571
		public virtual bool ShouldFlashOnPlayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600494D RID: 18765 RVA: 0x00205374 File Offset: 0x00203574
		protected void InvokeDisplayAmountChanged()
		{
			Action displayAmountChanged = this.DisplayAmountChanged;
			if (displayAmountChanged == null)
			{
				return;
			}
			displayAmountChanged();
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x0600494E RID: 18766 RVA: 0x00205388 File Offset: 0x00203588
		// (remove) Token: 0x0600494F RID: 18767 RVA: 0x002053C0 File Offset: 0x002035C0
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action DisplayAmountChanged;

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06004950 RID: 18768 RVA: 0x002053F8 File Offset: 0x002035F8
		// (remove) Token: 0x06004951 RID: 18769 RVA: 0x00205430 File Offset: 0x00203630
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action StatusChanged;

		// Token: 0x06004952 RID: 18770 RVA: 0x00205468 File Offset: 0x00203668
		public void UpdateTexture(TextureRect texture)
		{
			ShaderMaterial shaderMaterial = (ShaderMaterial)texture.Material;
			shaderMaterial.SetShaderParameter(RelicModel._isWaxStr, (this.IsWax > false) ? 1 : 0);
			if (this.IsMelted)
			{
				texture.SelfModulate = Colors.DarkRed;
			}
			if (!RunManager.Instance.IsInProgress || this.IsMelted)
			{
				shaderMaterial.SetShaderParameter(RelicModel._pulse, 0);
				shaderMaterial.SetShaderParameter(RelicModel._isUsed, 0);
				return;
			}
			switch (this.Status)
			{
			case RelicStatus.Normal:
				shaderMaterial.SetShaderParameter(RelicModel._pulse, 0);
				shaderMaterial.SetShaderParameter(RelicModel._isUsed, 0);
				return;
			case RelicStatus.Active:
				shaderMaterial.SetShaderParameter(RelicModel._pulse, 1);
				shaderMaterial.SetShaderParameter(RelicModel._isUsed, 0);
				return;
			case RelicStatus.Disabled:
				shaderMaterial.SetShaderParameter(RelicModel._pulse, 0);
				shaderMaterial.SetShaderParameter(RelicModel._isUsed, 1);
				return;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004953 RID: 18771 RVA: 0x00205570 File Offset: 0x00203770
		public HoverTip HoverTip
		{
			get
			{
				LocString locString = this.DynamicDescription;
				if (this.IsMelted)
				{
					locString = new LocString("gameplay_ui", "RELIC_IS_MELTED");
					locString.Add("description", this.DynamicDescription);
				}
				else if (this.IsUsedUp && base.IsMutable)
				{
					locString = new LocString("gameplay_ui", "RELIC_USED_UP");
					locString.Add("description", this.DynamicDescription);
				}
				HoverTip hoverTip = new HoverTip(this.Title, locString, null);
				hoverTip.SetCanonicalModel(this.CanonicalInstance);
				return hoverTip;
			}
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x002055FD File Offset: 0x002037FD
		protected virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06004955 RID: 18773 RVA: 0x00205604 File Offset: 0x00203804
		public IEnumerable<IHoverTip> HoverTipsExcludingRelic
		{
			get
			{
				return this.ExtraHoverTips;
			}
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06004956 RID: 18774 RVA: 0x0020560C File Offset: 0x0020380C
		public unsafe IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				int num = 1;
				List<IHoverTip> list = new List<IHoverTip>(num);
				CollectionsMarshal.SetCount<IHoverTip>(list, num);
				Span<IHoverTip> span = CollectionsMarshal.AsSpan<IHoverTip>(list);
				int num2 = 0;
				*span[num2] = this.HoverTip;
				List<IHoverTip> list2 = list;
				list2.AddRange(this.ExtraHoverTips);
				return list2;
			}
		}

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06004957 RID: 18775 RVA: 0x00205653 File Offset: 0x00203853
		// (set) Token: 0x06004958 RID: 18776 RVA: 0x00205665 File Offset: 0x00203865
		public RelicModel CanonicalInstance
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

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06004959 RID: 18777 RVA: 0x00205674 File Offset: 0x00203874
		// (set) Token: 0x0600495A RID: 18778 RVA: 0x0020567C File Offset: 0x0020387C
		public bool HasBeenRemovedFromState { get; private set; }

		// Token: 0x0600495B RID: 18779 RVA: 0x00205688 File Offset: 0x00203888
		public RelicModel ToMutable()
		{
			base.AssertCanonical();
			return (RelicModel)base.MutableClone();
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x002056A8 File Offset: 0x002038A8
		protected override void DeepCloneFields()
		{
			base.DeepCloneFields();
			this._dynamicVars = this.DynamicVars.Clone(this);
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x002056C2 File Offset: 0x002038C2
		protected override void AfterCloned()
		{
			base.AfterCloned();
			if (this._canonicalInstance == null)
			{
				this.CanonicalInstance = ModelDb.GetById<RelicModel>(base.Id);
			}
			this.HasBeenRemovedFromState = false;
			this.Flashed = null;
			this.DisplayAmountChanged = null;
			this.StatusChanged = null;
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x002056FF File Offset: 0x002038FF
		public void RemoveInternal()
		{
			this.HasBeenRemovedFromState = true;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x00205708 File Offset: 0x00203908
		public void IncrementStackCount()
		{
			base.AssertMutable();
			if (!this.IsStackable)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(69, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Cannot increment stack count on ");
				defaultInterpolatedStringHandler.AppendFormatted<ModelId>(base.Id);
				defaultInterpolatedStringHandler.AppendLiteral(" because it is not a stackable relic.");
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			int stackCount = this.StackCount;
			this.StackCount = stackCount + 1;
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x0020576F File Offset: 0x0020396F
		public virtual Task AfterObtained()
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x00205776 File Offset: 0x00203976
		public virtual Task AfterRemoved()
		{
			return Task.CompletedTask;
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x0020577D File Offset: 0x0020397D
		public SerializableRelic ToSerializable()
		{
			base.AssertMutable();
			return new SerializableRelic
			{
				Id = base.Id,
				Props = SavedProperties.From(this),
				FloorAddedToDeck = new int?(this.FloorAddedToDeck)
			};
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x002057B4 File Offset: 0x002039B4
		public static RelicModel FromSerializable(SerializableRelic save)
		{
			RelicModel relicModel = SaveUtil.RelicOrDeprecated(save.Id).ToMutable();
			SavedProperties props = save.Props;
			if (props != null)
			{
				props.Fill(relicModel);
			}
			if (save.FloorAddedToDeck != null)
			{
				relicModel.FloorAddedToDeck = save.FloorAddedToDeck.Value;
			}
			return relicModel;
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x00205809 File Offset: 0x00203A09
		protected void RelicIconChanged()
		{
			this._resolvedBigIconPath = null;
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06004965 RID: 18789 RVA: 0x00205812 File Offset: 0x00203A12
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x00205815 File Offset: 0x00203A15
		protected static LocString L10NLookup(string entryName)
		{
			return new LocString("relics", entryName);
		}

		// Token: 0x04001B2A RID: 6954
		private static readonly StringName _isUsed = new StringName("is_used");

		// Token: 0x04001B2B RID: 6955
		private static readonly StringName _pulse = new StringName("pulse");

		// Token: 0x04001B2C RID: 6956
		private static readonly StringName _isWaxStr = new StringName("is_wax");

		// Token: 0x04001B2D RID: 6957
		protected const string _locTable = "relics";

		// Token: 0x04001B2E RID: 6958
		[Nullable(2)]
		private string _resolvedBigIconPath;

		// Token: 0x04001B2F RID: 6959
		[Nullable(2)]
		private Player _owner;

		// Token: 0x04001B30 RID: 6960
		private bool _isWax;

		// Token: 0x04001B31 RID: 6961
		private bool _isMelted;

		// Token: 0x04001B33 RID: 6963
		[Nullable(2)]
		private DynamicVarSet _dynamicVars;

		// Token: 0x04001B34 RID: 6964
		private int _floorAddedToDeck;

		// Token: 0x04001B35 RID: 6965
		private RelicStatus _status;

		// Token: 0x04001B39 RID: 6969
		[Nullable(2)]
		private RelicModel _canonicalInstance;
	}
}
