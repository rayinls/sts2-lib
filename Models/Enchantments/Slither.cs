using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000873 RID: 2163
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Slither : EnchantmentModel
	{
		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x060065F3 RID: 26099 RVA: 0x00252F6B File Offset: 0x0025116B
		// (set) Token: 0x060065F4 RID: 26100 RVA: 0x00252F73 File Offset: 0x00251173
		public int TestEnergyCostOverride
		{
			get
			{
				return this._testEnergyCostOverride;
			}
			set
			{
				if (TestMode.IsOff)
				{
					throw new InvalidOperationException("Only set this value in test mode.");
				}
				base.AssertMutable();
				this._testEnergyCostOverride = value;
			}
		}

		// Token: 0x060065F5 RID: 26101 RVA: 0x00252F94 File Offset: 0x00251194
		public override bool CanEnchant(CardModel card)
		{
			return base.CanEnchant(card) && !card.Keywords.Contains(CardKeyword.Unplayable) && !card.EnergyCost.CostsX;
		}

		// Token: 0x060065F6 RID: 26102 RVA: 0x00252FC0 File Offset: 0x002511C0
		public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card != base.Card)
			{
				return Task.CompletedTask;
			}
			if (base.Card.Pile.Type != PileType.Hand)
			{
				return Task.CompletedTask;
			}
			base.Card.EnergyCost.SetThisCombat(this.NextEnergyCost(), false);
			NCard ncard = NCard.FindOnTable(card, null);
			if (ncard != null)
			{
				ncard.PlayRandomizeCostAnim();
			}
			return Task.CompletedTask;
		}

		// Token: 0x060065F7 RID: 26103 RVA: 0x0025302B File Offset: 0x0025122B
		private int NextEnergyCost()
		{
			if (this.TestEnergyCostOverride >= 0)
			{
				return this.TestEnergyCostOverride;
			}
			return base.Card.Owner.RunState.Rng.CombatEnergyCosts.NextInt(4);
		}

		// Token: 0x0400254B RID: 9547
		private int _testEnergyCostOverride = -1;
	}
}
