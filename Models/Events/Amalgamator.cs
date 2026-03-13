using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C2 RID: 1986
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Amalgamator : EventModel
	{
		// Token: 0x06006104 RID: 24836 RVA: 0x00244237 File Offset: 0x00242437
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All(delegate(Player p)
			{
				if (p.Deck.Cards.Count((CardModel c) => Amalgamator.IsValid(CardTag.Strike, c)) >= 2)
				{
					return p.Deck.Cards.Count((CardModel c) => Amalgamator.IsValid(CardTag.Defend, c)) >= 2;
				}
				return false;
			});
		}

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x06006105 RID: 24837 RVA: 0x00244263 File Offset: 0x00242463
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Card1", ModelDb.Card<UltimateStrike>().Title),
					new StringVar("Card2", ModelDb.Card<UltimateDefend>().Title)
				});
			}
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x002442A0 File Offset: 0x002424A0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.CombineStrikes), base.InitialOptionKey("COMBINE_STRIKES"), HoverTipFactory.FromCardWithCardHoverTips<UltimateStrike>(false)),
				new EventOption(this, new Func<Task>(this.CombineDefends), base.InitialOptionKey("COMBINE_DEFENDS"), HoverTipFactory.FromCardWithCardHoverTips<UltimateDefend>(false))
			});
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x00244304 File Offset: 0x00242504
		private async Task CombineStrikes()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 2);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, (CardModel c) => Amalgamator.IsValid(CardTag.Strike, c));
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			NDebugAudioManager instance = NDebugAudioManager.Instance;
			if (instance != null)
			{
				instance.Play("card_smith.mp3", 1f, PitchVariance.Small);
			}
			NGame.Instance.ScreenShakeTrauma(ShakeStrength.Strong);
			await Task.Delay(300);
			NDebugAudioManager instance2 = NDebugAudioManager.Instance;
			if (instance2 != null)
			{
				instance2.Play("card_smith.mp3", 1f, PitchVariance.Small);
			}
			NGame.Instance.ScreenShakeTrauma(ShakeStrength.Strong);
			CardModel cardModel = base.Owner.RunState.CreateCard<UltimateStrike>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("AMALGAMATOR.pages.COMBINE_STRIKES.description"));
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x00244348 File Offset: 0x00242548
		private async Task CombineDefends()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 2);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, (CardModel c) => Amalgamator.IsValid(CardTag.Defend, c));
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			NDebugAudioManager instance = NDebugAudioManager.Instance;
			if (instance != null)
			{
				instance.Play("card_smith.mp3", 1f, PitchVariance.Small);
			}
			NGame.Instance.ScreenShakeTrauma(ShakeStrength.Strong);
			await Task.Delay(300);
			NDebugAudioManager instance2 = NDebugAudioManager.Instance;
			if (instance2 != null)
			{
				instance2.Play("card_smith.mp3", 1f, PitchVariance.Small);
			}
			NGame.Instance.ScreenShakeTrauma(ShakeStrength.Strong);
			CardModel cardModel = base.Owner.RunState.CreateCard<UltimateDefend>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("AMALGAMATOR.pages.COMBINE_DEFENDS.description"));
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x0024438B File Offset: 0x0024258B
		private static bool IsValid(CardTag tag, CardModel card)
		{
			return card.Tags.Contains(tag) && (card != null && card.Rarity == CardRarity.Basic) && card.IsRemovable;
		}
	}
}
