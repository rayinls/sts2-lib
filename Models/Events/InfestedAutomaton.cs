using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D7 RID: 2007
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfestedAutomaton : EventModel
	{
		// Token: 0x060061A9 RID: 25001 RVA: 0x00246EB0 File Offset: 0x002450B0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Study), "INFESTED_AUTOMATON.pages.INITIAL.options.STUDY", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.TouchCore), "INFESTED_AUTOMATON.pages.INITIAL.options.TOUCH_CORE", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060061AA RID: 25002 RVA: 0x00246F06 File Offset: 0x00245106
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x060061AB RID: 25003 RVA: 0x00246F10 File Offset: 0x00245110
		private async Task Study()
		{
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithDefaultOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), (CardModel c) => c.Type == CardType.Power);
			CardCreationResult cardCreationResult = CardFactory.CreateForReward(base.Owner, 1, cardCreationOptions).FirstOrDefault<CardCreationResult>();
			CardModel cardModel = ((cardCreationResult != null) ? cardCreationResult.Card : null);
			if (cardModel != null)
			{
				CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.EventLayout);
			}
			base.SetEventFinished(base.L10NLookup("INFESTED_AUTOMATON.pages.STUDY.description"));
		}

		// Token: 0x060061AC RID: 25004 RVA: 0x00246F54 File Offset: 0x00245154
		private async Task TouchCore()
		{
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithDefaultOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), delegate(CardModel c)
			{
				CardEnergyCost energyCost = c.EnergyCost;
				return energyCost != null && energyCost.Canonical == 0 && !energyCost.CostsX;
			});
			CardCreationResult cardCreationResult = CardFactory.CreateForReward(base.Owner, 1, cardCreationOptions).FirstOrDefault<CardCreationResult>();
			CardModel cardModel = ((cardCreationResult != null) ? cardCreationResult.Card : null);
			if (cardModel != null)
			{
				CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.EventLayout);
			}
			base.SetEventFinished(base.L10NLookup("INFESTED_AUTOMATON.pages.TOUCH_CORE.description"));
		}
	}
}
