using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D0 RID: 2000
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DoorsOfLightAndDark : EventModel
	{
		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x0600616B RID: 24939 RVA: 0x00245DD4 File Offset: 0x00243FD4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x0600616C RID: 24940 RVA: 0x00245DE4 File Offset: 0x00243FE4
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Light), "DOORS_OF_LIGHT_AND_DARK.pages.INITIAL.options.LIGHT", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Dark), "DOORS_OF_LIGHT_AND_DARK.pages.INITIAL.options.DARK", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x0600616D RID: 24941 RVA: 0x00245E3C File Offset: 0x0024403C
		private Task Light()
		{
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsUpgradable).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.Niche)
				.Take(base.DynamicVars.Cards.IntValue);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			base.SetEventFinished(base.L10NLookup("DOORS_OF_LIGHT_AND_DARK.pages.LIGHT.description"));
			return Task.CompletedTask;
		}

		// Token: 0x0600616E RID: 24942 RVA: 0x00245F04 File Offset: 0x00244104
		private async Task Dark()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			base.SetEventFinished(base.L10NLookup("DOORS_OF_LIGHT_AND_DARK.pages.DARK.description"));
		}
	}
}
