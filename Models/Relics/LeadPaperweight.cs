using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Runs.History;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000529 RID: 1321
	public sealed class LeadPaperweight : RelicModel
	{
		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06004CB9 RID: 19641 RVA: 0x002162A1 File Offset: 0x002144A1
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x002162A4 File Offset: 0x002144A4
		[NullableContext(1)]
		public override async Task AfterObtained()
		{
			CardCreationOptions cardCreationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(ModelDb.CardPool<ColorlessCardPool>()), CardCreationSource.Other, CardRarityOddsType.RegularEncounter, null);
			List<CardModel> options = (from c in CardFactory.CreateForReward(base.Owner, 2, cardCreationOptions)
				select c.Card).ToList<CardModel>();
			CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(new BlockingPlayerChoiceContext(), options, base.Owner, true);
			CardModel chosenCard = cardModel;
			if (chosenCard != null)
			{
				CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(chosenCard, PileType.Deck, CardPilePosition.Bottom, null, false), 1.2f, CardPreviewStyle.HorizontalLayout);
			}
			foreach (CardModel cardModel2 in options)
			{
				if (cardModel2 != chosenCard)
				{
					MapPointHistoryEntry currentMapPointHistoryEntry = base.Owner.RunState.CurrentMapPointHistoryEntry;
					if (currentMapPointHistoryEntry != null)
					{
						currentMapPointHistoryEntry.GetEntry(base.Owner.NetId).CardChoices.Add(new CardChoiceHistoryEntry(cardModel2, false));
					}
				}
			}
		}
	}
}
