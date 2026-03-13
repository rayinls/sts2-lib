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
	// Token: 0x020006BB RID: 1723
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TemporaryFocusPower : PowerModel, ITemporaryPower
	{
		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06005638 RID: 22072 RVA: 0x00228053 File Offset: 0x00226253
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

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06005639 RID: 22073 RVA: 0x00228060 File Offset: 0x00226260
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x0600563A RID: 22074
		public abstract AbstractModel OriginModel { get; }

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x0600563B RID: 22075 RVA: 0x00228063 File Offset: 0x00226263
		public PowerModel InternallyAppliedPower
		{
			get
			{
				return ModelDb.Power<FocusPower>();
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x0600563C RID: 22076 RVA: 0x0022806A File Offset: 0x0022626A
		protected virtual bool IsPositive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x0600563D RID: 22077 RVA: 0x0022806D File Offset: 0x0022626D
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

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x0600563E RID: 22078 RVA: 0x0022807C File Offset: 0x0022627C
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

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x0600563F RID: 22079 RVA: 0x002280D6 File Offset: 0x002262D6
		public override LocString Description
		{
			get
			{
				return new LocString("powers", this.IsPositive ? "TEMPORARY_FOCUS_POWER.description" : "TEMPORARY_FOCUS_DOWN.description");
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06005640 RID: 22080 RVA: 0x002280F6 File Offset: 0x002262F6
		protected override string SmartDescriptionLocKey
		{
			get
			{
				if (!this.IsPositive)
				{
					return "TEMPORARY_FOCUS_DOWN.smartDescription";
				}
				return "TEMPORARY_FOCUS_POWER.smartDescription";
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06005641 RID: 22081 RVA: 0x0022810C File Offset: 0x0022630C
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
				list.Add(HoverTipFactory.FromPower<FocusPower>());
				return new <>z__ReadOnlyList<IHoverTip>(list);
			}
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x00228198 File Offset: 0x00226398
		public void IgnoreNextInstance()
		{
			this._shouldIgnoreNextInstance = true;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x002281A4 File Offset: 0x002263A4
		public override async Task BeforeApplied(Creature target, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (this._shouldIgnoreNextInstance)
			{
				this._shouldIgnoreNextInstance = false;
			}
			else
			{
				await PowerCmd.Apply<FocusPower>(target, this.Sign * amount, applier, cardSource, true);
			}
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x00228208 File Offset: 0x00226408
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
						await PowerCmd.Apply<FocusPower>(base.Owner, this.Sign * amount, applier, cardSource, true);
					}
				}
			}
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x0022826C File Offset: 0x0022646C
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Remove(this);
				await PowerCmd.Apply<FocusPower>(base.Owner, -this.Sign * base.Amount, base.Owner, null, false);
			}
		}

		// Token: 0x04002278 RID: 8824
		private bool _shouldIgnoreNextInstance;
	}
}
