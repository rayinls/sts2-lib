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
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E8 RID: 2024
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SelfHelpBook : EventModel
	{
		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06006248 RID: 25160 RVA: 0x0024A2C8 File Offset: 0x002484C8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Enchantment1", ModelDb.Enchantment<Sharp>().Title.GetFormattedText()),
					new StringVar("Enchantment2", ModelDb.Enchantment<Nimble>().Title.GetFormattedText()),
					new StringVar("Enchantment3", ModelDb.Enchantment<Swift>().Title.GetFormattedText()),
					new IntVar("Enchantment1Amount", 2m),
					new IntVar("Enchantment2Amount", 2m),
					new IntVar("Enchantment3Amount", 2m)
				});
			}
		}

		// Token: 0x06006249 RID: 25161 RVA: 0x0024A370 File Offset: 0x00248570
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = new List<EventOption>();
			bool flag = this.PlayerHasCardsAvailable<Sharp>(base.Owner, CardType.Attack);
			bool flag2 = this.PlayerHasCardsAvailable<Nimble>(base.Owner, CardType.Skill);
			bool flag3 = this.PlayerHasCardsAvailable<Swift>(base.Owner, CardType.Power);
			if (flag || flag2 || flag3)
			{
				if (flag)
				{
					list.Add(new EventOption(this, new Func<Task>(this.ReadTheBack), "SELF_HELP_BOOK.pages.INITIAL.options.READ_THE_BACK", HoverTipFactory.FromEnchantment<Sharp>(2)));
				}
				else
				{
					list.Add(new EventOption(this, null, "SELF_HELP_BOOK.pages.INITIAL.options.READ_THE_BACK_LOCKED", Array.Empty<IHoverTip>()));
				}
				if (flag2)
				{
					list.Add(new EventOption(this, new Func<Task>(this.ReadPassage), "SELF_HELP_BOOK.pages.INITIAL.options.READ_PASSAGE", HoverTipFactory.FromEnchantment<Nimble>(2)));
				}
				else
				{
					list.Add(new EventOption(this, null, "SELF_HELP_BOOK.pages.INITIAL.options.READ_PASSAGE_LOCKED", Array.Empty<IHoverTip>()));
				}
				if (flag3)
				{
					list.Add(new EventOption(this, new Func<Task>(this.ReadEntireBook), "SELF_HELP_BOOK.pages.INITIAL.options.READ_ENTIRE_BOOK", HoverTipFactory.FromEnchantment<Swift>(2)));
				}
				else
				{
					list.Add(new EventOption(this, null, "SELF_HELP_BOOK.pages.INITIAL.options.READ_ENTIRE_BOOK_LOCKED", Array.Empty<IHoverTip>()));
				}
			}
			else
			{
				list.Add(new EventOption(this, new Func<Task>(this.SkipBook), "SELF_HELP_BOOK.pages.INITIAL.options.NO_OPTIONS", Array.Empty<IHoverTip>()));
			}
			return list;
		}

		// Token: 0x0600624A RID: 25162 RVA: 0x0024A49C File Offset: 0x0024869C
		private async Task ReadTheBack()
		{
			await this.SelectAndEnchant<Sharp>(2, CardType.Attack, base.L10NLookup("SELF_HELP_BOOK.pages.READ_THE_BACK.description"));
		}

		// Token: 0x0600624B RID: 25163 RVA: 0x0024A4E0 File Offset: 0x002486E0
		private async Task ReadPassage()
		{
			await this.SelectAndEnchant<Nimble>(2, CardType.Skill, base.L10NLookup("SELF_HELP_BOOK.pages.READ_PASSAGE.description"));
		}

		// Token: 0x0600624C RID: 25164 RVA: 0x0024A524 File Offset: 0x00248724
		private async Task ReadEntireBook()
		{
			await this.SelectAndEnchant<Swift>(2, CardType.Power, base.L10NLookup("SELF_HELP_BOOK.pages.READ_ENTIRE_BOOK.description"));
		}

		// Token: 0x0600624D RID: 25165 RVA: 0x0024A568 File Offset: 0x00248768
		private bool PlayerHasCardsAvailable<[Nullable(0)] T>(Player player, CardType typeRestriction) where T : EnchantmentModel
		{
			EnchantmentModel enchantment = ModelDb.Enchantment<T>();
			return PileType.Deck.GetPile(player).Cards.FirstOrDefault((CardModel c) => this.DeckFilter(c, enchantment, typeRestriction)) != null;
		}

		// Token: 0x0600624E RID: 25166 RVA: 0x0024A5BC File Offset: 0x002487BC
		private async Task SelectAndEnchant<[Nullable(0)] T>(int amount, CardType typeRestriction, LocString finalDescription) where T : EnchantmentModel
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			EnchantmentModel enchantmentModel = ModelDb.Enchantment<T>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, enchantmentModel, amount, (CardModel c) => c.Type == typeRestriction, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await this.ApplyEnchantment<T>(cardModel, amount);
			}
			base.SetEventFinished(finalDescription);
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x0024A617 File Offset: 0x00248817
		private bool DeckFilter(CardModel card, EnchantmentModel enchantment, CardType type)
		{
			return card.Pile.Type == PileType.Deck && card.Type == type && enchantment.CanEnchant(card);
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x0024A63C File Offset: 0x0024883C
		private Task ApplyEnchantment<[Nullable(0)] T>(CardModel card, int amount) where T : EnchantmentModel
		{
			CardCmd.Enchant<T>(card, amount);
			NCardEnchantVfx ncardEnchantVfx = NCardEnchantVfx.Create(card);
			if (ncardEnchantVfx != null)
			{
				NRun instance = NRun.Instance;
				if (instance != null)
				{
					instance.GlobalUi.CardPreviewContainer.AddChildSafely(ncardEnchantVfx);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x06006251 RID: 25169 RVA: 0x0024A680 File Offset: 0x00248880
		private Task SkipBook()
		{
			base.SetEventFinished(base.L10NLookup("SELF_HELP_BOOK.pages.NO_OPTIONS.description"));
			return Task.CompletedTask;
		}

		// Token: 0x040024C0 RID: 9408
		private const string _readTheBackDescriptionKey = "SELF_HELP_BOOK.pages.READ_THE_BACK.description";

		// Token: 0x040024C1 RID: 9409
		private const string _readPassageDescriptionKey = "SELF_HELP_BOOK.pages.READ_PASSAGE.description";

		// Token: 0x040024C2 RID: 9410
		private const string _readEntireBookDescriptionKey = "SELF_HELP_BOOK.pages.READ_ENTIRE_BOOK.description";

		// Token: 0x040024C3 RID: 9411
		private const int _sharpAmount = 2;

		// Token: 0x040024C4 RID: 9412
		private const int _nimbleAmount = 2;

		// Token: 0x040024C5 RID: 9413
		private const int _swiftAmount = 2;
	}
}
