using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D5 RID: 2005
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GraveOfTheForgotten : EventModel
	{
		// Token: 0x060061A0 RID: 24992 RVA: 0x00246C20 File Offset: 0x00244E20
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			EnchantmentModel soulsPower = ModelDb.Enchantment<SoulsPower>();
			EventOption eventOption;
			if (PileType.Deck.GetPile(base.Owner).Cards.Any((CardModel c) => soulsPower.CanEnchant(c)))
			{
				eventOption = new EventOption(this, new Func<Task>(this.Confront), "GRAVE_OF_THE_FORGOTTEN.pages.INITIAL.options.CONFRONT", HoverTipFactory.FromEnchantment<SoulsPower>(1).Append(HoverTipFactory.FromCard<Decay>(false)));
			}
			else
			{
				eventOption = new EventOption(this, null, "GRAVE_OF_THE_FORGOTTEN.pages.INITIAL.options.CONFRONT_LOCKED", Array.Empty<IHoverTip>());
			}
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				eventOption,
				new EventOption(this, new Func<Task>(this.Accept), "GRAVE_OF_THE_FORGOTTEN.pages.INITIAL.options.ACCEPT", HoverTipFactory.FromRelic<ForgottenSoul>())
			});
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060061A1 RID: 24993 RVA: 0x00246CD0 File Offset: 0x00244ED0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Relic", ModelDb.Relic<ForgottenSoul>().Title.GetFormattedText()),
					new StringVar("Enchantment", ModelDb.Enchantment<SoulsPower>().Title.GetFormattedText()),
					new StringVar("Curse", ModelDb.Card<Decay>().Title)
				});
			}
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x00246D38 File Offset: 0x00244F38
		private async Task Confront()
		{
			await CardPileCmd.AddCurseToDeck<Decay>(base.Owner);
			CardModel cardModel = (await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<SoulsPower>(), 1, new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1))).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<SoulsPower>(cardModel, 1m);
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
			base.SetEventFinished(base.L10NLookup("GRAVE_OF_THE_FORGOTTEN.pages.CONFRONT.description"));
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x00246D7C File Offset: 0x00244F7C
		private async Task Accept()
		{
			await RelicCmd.Obtain<ForgottenSoul>(base.Owner);
			base.SetEventFinished(base.L10NLookup("GRAVE_OF_THE_FORGOTTEN.pages.ACCEPT.description"));
		}

		// Token: 0x0400249B RID: 9371
		private const string _enchantmentKey = "Enchantment";

		// Token: 0x0400249C RID: 9372
		private const string _relicKey = "Relic";

		// Token: 0x0400249D RID: 9373
		private const string _curseKey = "Curse";
	}
}
