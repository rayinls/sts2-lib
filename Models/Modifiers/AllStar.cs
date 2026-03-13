using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007AC RID: 1964
	[NullableContext(1)]
	[Nullable(0)]
	public class AllStar : ModifierModel
	{
		// Token: 0x060060BF RID: 24767 RVA: 0x002434F8 File Offset: 0x002416F8
		public override Func<Task> GenerateNeowOption(EventModel eventModel)
		{
			return () => AllStar.ObtainCards(eventModel.Owner, eventModel.Rng);
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x00243520 File Offset: 0x00241720
		private static async Task ObtainCards(Player player, Rng rng)
		{
			List<CardPileAddResult> results = new List<CardPileAddResult>();
			for (int i = 0; i < 5; i++)
			{
				CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(ModelDb.CardPool<ColorlessCardPool>()), null).WithFlags(CardCreationFlags.NoRarityModification | CardCreationFlags.NoCardPoolModifications);
				CardModel card = CardFactory.CreateForReward(player, 1, cardCreationOptions).First<CardCreationResult>().Card;
				CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Deck, CardPilePosition.Bottom, null, false);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				results.Add(cardPileAddResult2);
			}
			CardCmd.PreviewCardPileAdd(results, 1.2f, CardPreviewStyle.HorizontalLayout);
			await Cmd.CustomScaledWait(0.6f, 1.2f, false, default(CancellationToken));
		}
	}
}
