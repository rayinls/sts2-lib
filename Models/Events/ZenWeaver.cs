using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x02000804 RID: 2052
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ZenWeaver : EventModel
	{
		// Token: 0x0600635B RID: 25435 RVA: 0x0024F78F File Offset: 0x0024D98F
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Gold >= base.DynamicVars["EmotionalAwarenessCost"].BaseValue);
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0024F7A8 File Offset: 0x0024D9A8
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			int gold = base.Owner.Gold;
			EventOption eventOption = new EventOption(this, new Func<Task>(this.BreathingTechniques), "ZEN_WEAVER.pages.INITIAL.options.BREATHING_TECHNIQUES", HoverTipFactory.FromCardWithCardHoverTips<Enlightenment>(false));
			EventOption eventOption2;
			if (gold >= base.DynamicVars["EmotionalAwarenessCost"].IntValue)
			{
				eventOption2 = new EventOption(this, new Func<Task>(this.EmotionalAwareness), "ZEN_WEAVER.pages.INITIAL.options.EMOTIONAL_AWARENESS", Array.Empty<IHoverTip>());
			}
			else
			{
				eventOption2 = this.CreateLockedOption();
			}
			EventOption eventOption3;
			if (gold >= base.DynamicVars["ArachnidAcupunctureCost"].IntValue)
			{
				eventOption3 = new EventOption(this, new Func<Task>(this.ArachnidAcupuncture), "ZEN_WEAVER.pages.INITIAL.options.ARACHNID_ACUPUNCTURE", Array.Empty<IHoverTip>());
			}
			else
			{
				eventOption3 = this.CreateLockedOption();
			}
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[] { eventOption, eventOption2, eventOption3 });
		}

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x0600635D RID: 25437 RVA: 0x0024F874 File Offset: 0x0024DA74
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("BreathingTechniquesCost", 50m),
					new DynamicVar("EmotionalAwarenessCost", 125m),
					new DynamicVar("ArachnidAcupunctureCost", 250m)
				});
			}
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x0024F8CC File Offset: 0x0024DACC
		private async Task BreathingTechniques()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["BreathingTechniquesCost"].IntValue, base.Owner, GoldLossType.Spent);
			CardModel[] array = new CardModel[2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = base.Owner.RunState.CreateCard<Enlightenment>(base.Owner);
			}
			IReadOnlyList<CardPileAddResult> readOnlyList = await CardPileCmd.Add(array, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardCmd.PreviewCardPileAdd(readOnlyList, 1.2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("ZEN_WEAVER.pages.BREATHING_TECHNIQUES.description"));
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x0024F910 File Offset: 0x0024DB10
		private async Task EmotionalAwareness()
		{
			await this.RemoveCardsAndProceed(base.DynamicVars["EmotionalAwarenessCost"].IntValue, 1);
			base.SetEventFinished(base.L10NLookup("ZEN_WEAVER.pages.EMOTIONAL_AWARENESS.description"));
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x0024F954 File Offset: 0x0024DB54
		private async Task ArachnidAcupuncture()
		{
			await this.RemoveCardsAndProceed(base.DynamicVars["ArachnidAcupunctureCost"].IntValue, 2);
			base.SetEventFinished(base.L10NLookup("ZEN_WEAVER.pages.ARACHNID_ACUPUNCTURE.description"));
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x0024F998 File Offset: 0x0024DB98
		private async Task RemoveCardsAndProceed(int cost, int count)
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, count), null);
			IEnumerable<CardModel> enumerable2 = enumerable;
			await CardPileCmd.RemoveFromDeck(enumerable2.ToList<CardModel>(), true);
			await PlayerCmd.LoseGold(cost, base.Owner, GoldLossType.Spent);
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x0024F9EB File Offset: 0x0024DBEB
		private EventOption CreateLockedOption()
		{
			return new EventOption(this, null, "ZEN_WEAVER.pages.INITIAL.options.LOCKED", Array.Empty<IHoverTip>());
		}

		// Token: 0x0400250C RID: 9484
		private const string _breathingTechniquesCostKey = "BreathingTechniquesCost";

		// Token: 0x0400250D RID: 9485
		private const string _emotionalAwarenessCostKey = "EmotionalAwarenessCost";

		// Token: 0x0400250E RID: 9486
		private const string _arachnidAcupunctureCostKey = "ArachnidAcupunctureCost";
	}
}
