using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x0200049F RID: 1183
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class PotionModel : AbstractModel
	{
		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06004890 RID: 18576 RVA: 0x00203C30 File Offset: 0x00201E30
		// (remove) Token: 0x06004891 RID: 18577 RVA: 0x00203C68 File Offset: 0x00201E68
		[Nullable(2)]
		[method: NullableContext(2)]
		[field: Nullable(2)]
		public event Action BeforeUse;

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x00203C9D File Offset: 0x00201E9D
		public LocString Title
		{
			get
			{
				return new LocString("potions", base.Id.Entry + ".title");
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004893 RID: 18579 RVA: 0x00203CBE File Offset: 0x00201EBE
		public LocString Description
		{
			get
			{
				return new LocString("potions", base.Id.Entry + ".description");
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x00203CDF File Offset: 0x00201EDF
		public LocString SelectionScreenPrompt
		{
			get
			{
				return new LocString("potions", base.Id.Entry + ".selectionScreenPrompt");
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004895 RID: 18581 RVA: 0x00203D00 File Offset: 0x00201F00
		public LocString StaticDescription
		{
			get
			{
				return this.Description;
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x00203D08 File Offset: 0x00201F08
		public LocString DynamicDescription
		{
			get
			{
				LocString description = this.Description;
				this.DynamicVars.AddTo(description);
				string prefix = EnergyIconHelper.GetPrefix(this);
				description.Add("energyPrefix", EnergyIconHelper.GetPrefix(this));
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

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004897 RID: 18583 RVA: 0x00203D94 File Offset: 0x00201F94
		private string PackedImagePath
		{
			get
			{
				return ImageHelper.GetImagePath("atlases/potion_atlas.sprites/" + base.Id.Entry.ToLowerInvariant() + ".tres");
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x00203DBA File Offset: 0x00201FBA
		private string PackedOutlinePath
		{
			get
			{
				return ImageHelper.GetImagePath("atlases/potion_outline_atlas.sprites/" + base.Id.Entry.ToLowerInvariant() + ".tres");
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004899 RID: 18585 RVA: 0x00203DE0 File Offset: 0x00201FE0
		public string ImagePath
		{
			get
			{
				return this.PackedImagePath;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x00203DE8 File Offset: 0x00201FE8
		public Texture2D Image
		{
			get
			{
				return ResourceLoader.Load<Texture2D>(this.PackedImagePath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600489B RID: 18587 RVA: 0x00203DF8 File Offset: 0x00201FF8
		[Nullable(2)]
		public string OutlinePath
		{
			[NullableContext(2)]
			get
			{
				if (!ResourceLoader.Exists(this.PackedOutlinePath, ""))
				{
					return null;
				}
				return this.PackedOutlinePath;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600489C RID: 18588 RVA: 0x00203E14 File Offset: 0x00202014
		[Nullable(2)]
		public Texture2D Outline
		{
			[NullableContext(2)]
			get
			{
				if (this.OutlinePath == null)
				{
					return null;
				}
				return ResourceLoader.Load<Texture2D>(this.OutlinePath, null, ResourceLoader.CacheMode.Reuse);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600489D RID: 18589
		public abstract PotionRarity Rarity { get; }

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600489E RID: 18590
		public abstract PotionUsage Usage { get; }

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600489F RID: 18591
		public abstract TargetType TargetType { get; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x060048A0 RID: 18592 RVA: 0x00203E2E File Offset: 0x0020202E
		public PotionPoolModel Pool
		{
			get
			{
				return ModelDb.AllPotionPools.First((PotionPoolModel p) => p.AllPotionIds.Contains(base.Id));
			}
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x00203E46 File Offset: 0x00202046
		// (set) Token: 0x060048A2 RID: 18594 RVA: 0x00203E54 File Offset: 0x00202054
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
					throw new InvalidOperationException("Cannot move potion " + base.Id.Entry + " from one owner to another");
				}
				this._owner = value;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x060048A3 RID: 18595 RVA: 0x00203E94 File Offset: 0x00202094
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

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x060048A4 RID: 18596 RVA: 0x00203EC8 File Offset: 0x002020C8
		protected virtual IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x060048A5 RID: 18597 RVA: 0x00203ECF File Offset: 0x002020CF
		// (set) Token: 0x060048A6 RID: 18598 RVA: 0x00203ED7 File Offset: 0x002020D7
		public bool IsQueued { get; private set; }

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x060048A7 RID: 18599 RVA: 0x00203EE0 File Offset: 0x002020E0
		public virtual bool CanBeGeneratedInCombat
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x060048A8 RID: 18600 RVA: 0x00203EE3 File Offset: 0x002020E3
		public virtual bool PassesCustomUsabilityCheck
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x00203EE8 File Offset: 0x002020E8
		public HoverTip HoverTip
		{
			get
			{
				HoverTip hoverTip = new HoverTip(this.Title, this.DynamicDescription, null);
				hoverTip.SetCanonicalModel(this.CanonicalInstance);
				return hoverTip;
			}
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x060048AA RID: 18602 RVA: 0x00203F17 File Offset: 0x00202117
		public IEnumerable<IHoverTip> HoverTips
		{
			get
			{
				return new IHoverTip[] { this.HoverTip }.Concat(this.ExtraHoverTips);
			}
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x060048AB RID: 18603 RVA: 0x00203F38 File Offset: 0x00202138
		public virtual IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return Array.Empty<IHoverTip>();
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x060048AC RID: 18604 RVA: 0x00203F3F File Offset: 0x0020213F
		// (set) Token: 0x060048AD RID: 18605 RVA: 0x00203F51 File Offset: 0x00202151
		public PotionModel CanonicalInstance
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

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x060048AE RID: 18606 RVA: 0x00203F60 File Offset: 0x00202160
		public override bool ShouldReceiveCombatHooks
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x060048AF RID: 18607 RVA: 0x00203F63 File Offset: 0x00202163
		// (set) Token: 0x060048B0 RID: 18608 RVA: 0x00203F6B File Offset: 0x0020216B
		public bool HasBeenRemovedFromState { get; private set; }

		// Token: 0x060048B1 RID: 18609 RVA: 0x00203F74 File Offset: 0x00202174
		public PotionModel ToMutable()
		{
			base.AssertCanonical();
			PotionModel potionModel = (PotionModel)base.MutableClone();
			potionModel.CanonicalInstance = this;
			return potionModel;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x00203F9B File Offset: 0x0020219B
		protected override void AfterCloned()
		{
			base.AfterCloned();
			this.HasBeenRemovedFromState = false;
			this.BeforeUse = null;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x00203FB1 File Offset: 0x002021B1
		public void Discard()
		{
			this.Owner.DiscardPotionInternal(this, false);
			this.HasBeenRemovedFromState = true;
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x00203FC7 File Offset: 0x002021C7
		public void RemoveBeforeUse()
		{
			this.Owner.RemoveUsedPotionInternal(this);
			this.HasBeenRemovedFromState = true;
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x00203FDC File Offset: 0x002021DC
		[NullableContext(2)]
		public void EnqueueManualUse(Creature target)
		{
			base.AssertMutable();
			Action beforeUse = this.BeforeUse;
			if (beforeUse != null)
			{
				beforeUse();
			}
			UsePotionAction usePotionAction = new UsePotionAction(this, target, CombatManager.Instance.IsInProgress);
			this.IsQueued = true;
			RunManager.Instance.ActionQueueSynchronizer.RequestEnqueue(usePotionAction);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x0020402C File Offset: 0x0020222C
		public async Task OnUseWrapper(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			this.RemoveBeforeUse();
			CombatState combatState = this.Owner.Creature.CombatState;
			choiceContext.PushModel(this);
			await CombatManager.Instance.WaitForUnpause();
			await Hook.BeforePotionUsed(this.Owner.RunState, combatState, this, target);
			if (TestMode.IsOff && combatState != null)
			{
				NCreature creatureNode = NCombatRoom.Instance.GetCreatureNode(this.Owner.Creature);
				Vector2 vector = Vector2.Zero;
				if (this.TargetType.IsSingleTarget())
				{
					vector = NCombatRoom.Instance.GetCreatureNode(target).GetBottomOfHitbox();
				}
				else
				{
					IReadOnlyList<Creature> readOnlyList;
					if (this.TargetType == TargetType.AllEnemies)
					{
						readOnlyList = (from c in combatState.GetCreaturesOnSide(CombatSide.Enemy)
							where c.IsHittable
							select c).ToList<Creature>();
					}
					else
					{
						readOnlyList = (from c in combatState.GetCreaturesOnSide(CombatSide.Player)
							where c.IsHittable
							select c).ToList<Creature>();
					}
					foreach (Creature creature in readOnlyList)
					{
						vector += NCombatRoom.Instance.GetCreatureNode(creature).VfxSpawnPosition;
					}
					vector /= (float)readOnlyList.Count;
				}
				NItemThrowVfx nitemThrowVfx = NItemThrowVfx.Create(creatureNode.VfxSpawnPosition, vector, this.Image, null);
				NCombatRoom.Instance.CombatVfxContainer.AddChildSafely(nitemThrowVfx);
				await Cmd.Wait(0.5f, false);
			}
			await this.OnUse(choiceContext, target);
			base.InvokeExecutionFinished();
			if (combatState != null && CombatManager.Instance.IsInProgress)
			{
				CombatManager.Instance.History.PotionUsed(combatState, this, target);
			}
			await Hook.AfterPotionUsed(this.Owner.RunState, combatState, this, target);
			MapPointHistoryEntry currentMapPointHistoryEntry = this.Owner.RunState.CurrentMapPointHistoryEntry;
			if (currentMapPointHistoryEntry != null)
			{
				currentMapPointHistoryEntry.GetEntry(this.Owner.NetId).PotionUsed.Add(base.Id);
			}
			await CombatManager.Instance.CheckForEmptyHand(choiceContext, this.Owner);
			choiceContext.PopModel(this);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x0020407F File Offset: 0x0020227F
		public void AfterUsageCanceled()
		{
			this.IsQueued = false;
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x00204088 File Offset: 0x00202288
		protected virtual Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			return Task.CompletedTask;
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x0020408F File Offset: 0x0020228F
		public SerializablePotion ToSerializable(int slotIndex)
		{
			base.AssertMutable();
			return new SerializablePotion
			{
				Id = base.Id,
				SlotIndex = slotIndex
			};
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x002040AF File Offset: 0x002022AF
		public static PotionModel FromSerializable(SerializablePotion save)
		{
			return SaveUtil.PotionOrDeprecated(save.Id).ToMutable();
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x002040C1 File Offset: 0x002022C1
		[NullableContext(2)]
		protected static void AssertValidForTargetedPotion([NotNull] Creature target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target", "Target must be present for targeted potions.");
			}
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x002040D6 File Offset: 0x002022D6
		public bool CanThrowAtAlly()
		{
			return this.TargetType == TargetType.AnyPlayer && this.Owner.RunState.Players.Count > 1 && CombatManager.Instance.IsInProgress;
		}

		// Token: 0x04001B0F RID: 6927
		public const string locTable = "potions";

		// Token: 0x04001B11 RID: 6929
		[Nullable(2)]
		private Player _owner;

		// Token: 0x04001B12 RID: 6930
		[Nullable(2)]
		private DynamicVarSet _dynamicVars;

		// Token: 0x04001B14 RID: 6932
		private PotionModel _canonicalInstance;
	}
}
