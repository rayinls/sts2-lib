using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006BC RID: 1724
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TemporaryStrengthPower : PowerModel, ITemporaryPower
	{
		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06005647 RID: 22087 RVA: 0x002282BF File Offset: 0x002264BF
		public override PowerType Type
		{
			get
			{
				if (!this.IsPositive)
				{
					return PowerType.Debuff;
				}
				return PowerType.Buff;
			}
		}

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06005648 RID: 22088 RVA: 0x002282CC File Offset: 0x002264CC
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06005649 RID: 22089
		public abstract AbstractModel OriginModel { get; }

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x0600564A RID: 22090 RVA: 0x002282CF File Offset: 0x002264CF
		public PowerModel InternallyAppliedPower
		{
			get
			{
				return ModelDb.Power<StrengthPower>();
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x0600564B RID: 22091 RVA: 0x002282D6 File Offset: 0x002264D6
		protected virtual bool IsPositive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x0600564C RID: 22092 RVA: 0x002282D9 File Offset: 0x002264D9
		private int Sign
		{
			get
			{
				if (!this.IsPositive)
				{
					return -1;
				}
				return 1;
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x0600564D RID: 22093 RVA: 0x002282E8 File Offset: 0x002264E8
		public override LocString Title
		{
			get
			{
				AbstractModel originModel = this.OriginModel;
				CardModel cardModel = originModel as CardModel;
				LocString locString;
				if (cardModel == null)
				{
					PotionModel potionModel = originModel as PotionModel;
					if (potionModel == null)
					{
						RelicModel relicModel = originModel as RelicModel;
						if (relicModel == null)
						{
							throw new InvalidOperationException();
						}
						locString = relicModel.Title;
					}
					else
					{
						locString = potionModel.Title;
					}
				}
				else
				{
					locString = cardModel.TitleLocString;
				}
				return locString;
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x0600564E RID: 22094 RVA: 0x00228342 File Offset: 0x00226542
		public override LocString Description
		{
			get
			{
				return new LocString("powers", this.IsPositive ? "TEMPORARY_STRENGTH_POWER.description" : "TEMPORARY_STRENGTH_DOWN.description");
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x0600564F RID: 22095 RVA: 0x00228362 File Offset: 0x00226562
		protected override string SmartDescriptionLocKey
		{
			get
			{
				if (!this.IsPositive)
				{
					return "TEMPORARY_STRENGTH_DOWN.smartDescription";
				}
				return "TEMPORARY_STRENGTH_POWER.smartDescription";
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06005650 RID: 22096 RVA: 0x00228378 File Offset: 0x00226578
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				List<IHoverTip> list = new List<IHoverTip>();
				List<IHoverTip> list2 = list;
				AbstractModel originModel = this.OriginModel;
				CardModel cardModel = originModel as CardModel;
				IEnumerable<IHoverTip> enumerable;
				if (cardModel == null)
				{
					PotionModel potionModel = originModel as PotionModel;
					if (potionModel == null)
					{
						RelicModel relicModel = originModel as RelicModel;
						if (relicModel == null)
						{
							throw new InvalidOperationException();
						}
						enumerable = HoverTipFactory.FromRelic(relicModel);
					}
					else
					{
						enumerable = new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPotion(potionModel));
					}
				}
				else
				{
					enumerable = new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard(cardModel, false));
				}
				list2.AddRange(enumerable);
				list.Add(HoverTipFactory.FromPower<StrengthPower>());
				return new <>z__ReadOnlyList<IHoverTip>(list);
			}
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x00228404 File Offset: 0x00226604
		public void IgnoreNextInstance()
		{
			this._shouldIgnoreNextInstance = true;
		}

		// Token: 0x06005652 RID: 22098 RVA: 0x00228410 File Offset: 0x00226610
		public override async Task BeforeApplied(Creature target, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (this._shouldIgnoreNextInstance)
			{
				this._shouldIgnoreNextInstance = false;
			}
			else
			{
				await PowerCmd.Apply<StrengthPower>(target, this.Sign * amount, applier, cardSource, true);
			}
		}

		// Token: 0x06005653 RID: 22099 RVA: 0x00228474 File Offset: 0x00226674
		public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!(amount == base.Amount))
			{
				if (power == this)
				{
					if (this._shouldIgnoreNextInstance)
					{
						this._shouldIgnoreNextInstance = false;
					}
					else
					{
						await PowerCmd.Apply<StrengthPower>(base.Owner, this.Sign * amount, applier, cardSource, true);
					}
				}
			}
		}

		// Token: 0x06005654 RID: 22100 RVA: 0x002284D8 File Offset: 0x002266D8
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Remove(this);
				await PowerCmd.Apply<StrengthPower>(base.Owner, -this.Sign * base.Amount, base.Owner, null, false);
			}
		}

		// Token: 0x04002279 RID: 8825
		private bool _shouldIgnoreNextInstance;
	}
}
