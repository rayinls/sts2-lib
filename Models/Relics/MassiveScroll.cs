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
	// Token: 0x02000535 RID: 1333
	public sealed class MassiveScroll : RelicModel
	{
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x00216997 File Offset: 0x00214B97
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0021699C File Offset: 0x00214B9C
		[NullableContext(1)]
		public override async Task AfterObtained()
		{
			IEnumerable<CardModel> enumerable = from c in ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.RunState.UnlockState, base.Owner.RunState.CardMultiplayerConstraint).Concat(base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.RunState.UnlockState, base.Owner.RunState.CardMultiplayerConstraint))
				where c.MultiplayerConstraint == CardMultiplayerConstraint.MultiplayerOnly
				select c;
			CardCreationOptions cardCreationOptions = new CardCreationOptions(enumerable, CardCreationSource.Other, CardRarityOddsType.RegularEncounter);
			List<CardModel> options = (from r in CardFactory.CreateForReward(base.Owner, 3, cardCreationOptions)
				select r.Card).ToList<CardModel>();
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
