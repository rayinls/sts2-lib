using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B4 RID: 1972
	[NullableContext(1)]
	[Nullable(0)]
	public class Hoarder : ModifierModel
	{
		// Token: 0x060060D9 RID: 24793 RVA: 0x00243A20 File Offset: 0x00241C20
		public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			if (oldPileType == PileType.None)
			{
				CardPile pile = card.Pile;
				if (pile != null && pile.Type == PileType.Deck)
				{
					if (source == null)
					{
						if (!this._cardsToSkip.Remove(card))
						{
							for (int i = 0; i < 2; i++)
							{
								CardModel cardModel = card.Owner.RunState.CloneCard(card);
								this._cardsToSkip.Add(cardModel);
								CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, this, false);
								CardPileAddResult cardPileAddResult2 = cardPileAddResult;
								CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.HorizontalLayout);
							}
						}
					}
				}
			}
		}

		// Token: 0x060060DA RID: 24794 RVA: 0x00243A7B File Offset: 0x00241C7B
		public override bool ShouldAllowMerchantCardRemoval(Player player)
		{
			return false;
		}

		// Token: 0x04002467 RID: 9319
		private readonly HashSet<CardModel> _cardsToSkip = new HashSet<CardModel>();
	}
}
