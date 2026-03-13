using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D4 RID: 2004
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FieldOfManSizedHoles : EventModel
	{
		// Token: 0x0600619A RID: 24986 RVA: 0x00246AA9 File Offset: 0x00244CA9
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => CardPile.Get(PileType.Deck, p).Cards.Any(new Func<CardModel, bool>(ModelDb.Enchantment<PerfectFit>().CanEnchant)));
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x0600619B RID: 24987 RVA: 0x00246AD8 File Offset: 0x00244CD8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new GoldVar(75),
					new CardsVar(2),
					new StringVar("ResistCurse", ModelDb.Card<Normality>().Title),
					new StringVar("Enchantment", ModelDb.Enchantment<PerfectFit>().Title.GetFormattedText())
				});
			}
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x00246B38 File Offset: 0x00244D38
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Resist), "FIELD_OF_MAN_SIZED_HOLES.pages.INITIAL.options.RESIST", HoverTipFactory.FromCardWithCardHoverTips<Normality>(false)),
				new EventOption(this, new Func<Task>(this.EnterYourHole), "FIELD_OF_MAN_SIZED_HOLES.pages.INITIAL.options.ENTER_YOUR_HOLE", HoverTipFactory.FromEnchantment<PerfectFit>(1))
			});
		}

		// Token: 0x0600619D RID: 24989 RVA: 0x00246B90 File Offset: 0x00244D90
		private async Task Resist()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			await CardPileCmd.AddCursesToDeck(new <>z__ReadOnlySingleElementList<CardModel>(ModelDb.Card<Normality>()), base.Owner);
			base.SetEventFinished(base.L10NLookup("FIELD_OF_MAN_SIZED_HOLES.pages.RESIST.description"));
		}

		// Token: 0x0600619E RID: 24990 RVA: 0x00246BD4 File Offset: 0x00244DD4
		private async Task EnterYourHole()
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<PerfectFit>(), 1, new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1));
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<PerfectFit>(cardModel, 1m);
				NCardEnchantVfx ncardEnchantVfx = NCardEnchantVfx.Create(cardModel);
				if (ncardEnchantVfx != null)
				{
					NRun instance = NRun.Instance;
					if (instance != null)
					{
						instance.GlobalUi.CardPreviewContainer.AddChildSafely(ncardEnchantVfx);
					}
				}
			}
			base.SetEventFinished(base.L10NLookup("FIELD_OF_MAN_SIZED_HOLES.pages.ENTER_YOUR_HOLE.description"));
		}
	}
}
