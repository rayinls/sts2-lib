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
	// Token: 0x020006BA RID: 1722
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class TemporaryDexterityPower : PowerModel, ITemporaryPower
	{
		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06005629 RID: 22057 RVA: 0x00227DE8 File Offset: 0x00225FE8
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

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x0600562A RID: 22058 RVA: 0x00227DF5 File Offset: 0x00225FF5
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x0600562B RID: 22059
		public abstract AbstractModel OriginModel { get; }

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x0600562C RID: 22060 RVA: 0x00227DF8 File Offset: 0x00225FF8
		public PowerModel InternallyAppliedPower
		{
			get
			{
				return ModelDb.Power<DexterityPower>();
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x0600562D RID: 22061 RVA: 0x00227DFF File Offset: 0x00225FFF
		protected virtual bool IsPositive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x0600562E RID: 22062 RVA: 0x00227E02 File Offset: 0x00226002
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

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x0600562F RID: 22063 RVA: 0x00227E10 File Offset: 0x00226010
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

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06005630 RID: 22064 RVA: 0x00227E6A File Offset: 0x0022606A
		public override LocString Description
		{
			get
			{
				return new LocString("powers", this.IsPositive ? "TEMPORARY_DEXTERITY_POWER.description" : "TEMPORARY_DEXTERITY_DOWN.description");
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06005631 RID: 22065 RVA: 0x00227E8A File Offset: 0x0022608A
		protected override string SmartDescriptionLocKey
		{
			get
			{
				if (!this.IsPositive)
				{
					return "TEMPORARY_DEXTERITY_DOWN.smartDescription";
				}
				return "TEMPORARY_DEXTERITY_POWER.smartDescription";
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06005632 RID: 22066 RVA: 0x00227EA0 File Offset: 0x002260A0
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
				list.Add(HoverTipFactory.FromPower<DexterityPower>());
				return new <>z__ReadOnlyList<IHoverTip>(list);
			}
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x00227F2C File Offset: 0x0022612C
		public void IgnoreNextInstance()
		{
			this._shouldIgnoreNextInstance = true;
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x00227F38 File Offset: 0x00226138
		public override async Task BeforeApplied(Creature target, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (this._shouldIgnoreNextInstance)
			{
				this._shouldIgnoreNextInstance = false;
			}
			else
			{
				await PowerCmd.Apply<DexterityPower>(target, this.Sign * amount, applier, cardSource, true);
			}
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x00227F9C File Offset: 0x0022619C
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
						await PowerCmd.Apply<DexterityPower>(base.Owner, this.Sign * amount, applier, cardSource, true);
					}
				}
			}
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x00228000 File Offset: 0x00226200
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Remove(this);
				await PowerCmd.Apply<DexterityPower>(base.Owner, -this.Sign * base.Amount, base.Owner, null, false);
			}
		}

		// Token: 0x04002277 RID: 8823
		private bool _shouldIgnoreNextInstance;
	}
}
