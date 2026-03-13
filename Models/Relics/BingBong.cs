using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B8 RID: 1208
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BingBong : RelicModel
	{
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x060049F7 RID: 18935 RVA: 0x002110FB File Offset: 0x0020F2FB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x060049F8 RID: 18936 RVA: 0x002110FE File Offset: 0x0020F2FE
		private HashSet<CardModel> CardsToSkip
		{
			get
			{
				base.AssertMutable();
				if (this._cardsToSkip == null)
				{
					this._cardsToSkip = new HashSet<CardModel>();
				}
				return this._cardsToSkip;
			}
		}

		// Token: 0x060049F9 RID: 18937 RVA: 0x00211120 File Offset: 0x0020F320
		public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			CardPile pile = card.Pile;
			if (pile != null && pile.Type == PileType.Deck)
			{
				if (card.Owner == base.Owner)
				{
					if (source == null)
					{
						if (!this.CardsToSkip.Remove(card))
						{
							base.Flash();
							CardModel cardModel = base.Owner.RunState.CloneCard(card);
							this.CardsToSkip.Add(cardModel);
							CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, this, false);
							CardPileAddResult cardPileAddResult2 = cardPileAddResult;
							CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.HorizontalLayout);
						}
					}
				}
			}
		}

		// Token: 0x0400218B RID: 8587
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<CardModel> _cardsToSkip;
	}
}
