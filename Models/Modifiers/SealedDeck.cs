using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B9 RID: 1977
	[NullableContext(1)]
	[Nullable(0)]
	public class SealedDeck : ModifierModel
	{
		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x00243D5C File Offset: 0x00241F5C
		public override bool ClearsPlayerDeck
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060060EB RID: 24811 RVA: 0x00243D60 File Offset: 0x00241F60
		public override Func<Task> GenerateNeowOption(EventModel eventModel)
		{
			return () => SealedDeck.ChooseCards(eventModel.Owner);
		}

		// Token: 0x060060EC RID: 24812 RVA: 0x00243D88 File Offset: 0x00241F88
		private static async Task ChooseCards(Player player)
		{
			CardCreationOptions cardCreationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(player.Character.CardPool), CardCreationSource.Other, CardRarityOddsType.RegularEncounter, null).WithFlags(CardCreationFlags.NoUpgradeRoll | CardCreationFlags.ForceRarityOddsChange);
			IEnumerable<CardCreationResult> enumerable = CardFactory.CreateForReward(player, 30, cardCreationOptions).ToList<CardCreationResult>();
			enumerable = from r in enumerable
				orderby r.Card.Rarity, r.Card.Title
				select r;
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(new LocString("modifiers", "SEALED_DECK.selectionPrompt"), 10)
			{
				Cancelable = false,
				RequireManualConfirmation = true
			};
			IEnumerable<CardModel> enumerable2 = await CardSelectCmd.FromSimpleGridForRewards(new BlockingPlayerChoiceContext(), enumerable.ToList<CardCreationResult>(), player, cardSelectorPrefs);
			List<CardModel> list = enumerable2.ToList<CardModel>();
			IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.Add(list, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardCmd.PreviewCardPileAdd(readOnlyList, 1.2f, CardPreviewStyle.GridLayout);
			foreach (Player player2 in player.RunState.Players)
			{
				player2.RelicGrabBag.Remove<PandorasBox>();
			}
			player.RunState.SharedRelicGrabBag.Remove<PandorasBox>();
		}
	}
}
