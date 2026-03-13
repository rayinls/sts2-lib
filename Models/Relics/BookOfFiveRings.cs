using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C1 RID: 1217
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BookOfFiveRings : RelicModel
	{
		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x0021169D File Offset: 0x0020F89D
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x06004A25 RID: 18981 RVA: 0x002116A0 File Offset: 0x0020F8A0
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x002116A8 File Offset: 0x0020F8A8
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x002116AB File Offset: 0x0020F8AB
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.CardsAddedSinceLastTrigger;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06004A28 RID: 18984 RVA: 0x002116CC File Offset: 0x0020F8CC
		// (set) Token: 0x06004A29 RID: 18985 RVA: 0x002116D4 File Offset: 0x0020F8D4
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x002116E9 File Offset: 0x0020F8E9
		// (set) Token: 0x06004A2B RID: 18987 RVA: 0x002116F1 File Offset: 0x0020F8F1
		[SavedProperty]
		public int CardsAdded
		{
			get
			{
				return this._cardsAdded;
			}
			set
			{
				base.AssertMutable();
				this._cardsAdded = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06004A2C RID: 18988 RVA: 0x00211706 File Offset: 0x0020F906
		private int CardsAddedSinceLastTrigger
		{
			get
			{
				return this.CardsAdded % base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06004A2D RID: 18989 RVA: 0x0021171F File Offset: 0x0020F91F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(5),
					new HealVar(15m)
				});
			}
		}

		// Token: 0x06004A2E RID: 18990 RVA: 0x00211744 File Offset: 0x0020F944
		public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			if (!base.Owner.Creature.IsDead)
			{
				if (card.Owner == base.Owner)
				{
					CardPile pile = card.Pile;
					if (pile != null && pile.Type == PileType.Deck)
					{
						int cardsAdded = this.CardsAdded;
						this.CardsAdded = cardsAdded + 1;
						if (this.CardsAddedSinceLastTrigger == 0)
						{
							TaskHelper.RunSafely(this.DoActivateVisuals());
							await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
						}
					}
				}
			}
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x00211790 File Offset: 0x0020F990
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x0400218E RID: 8590
		private bool _isActivating;

		// Token: 0x0400218F RID: 8591
		private int _cardsAdded;
	}
}
