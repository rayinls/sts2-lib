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
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x02000803 RID: 2051
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WoodCarvings : EventModel
	{
		// Token: 0x06006354 RID: 25428 RVA: 0x0024F561 File Offset: 0x0024D761
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => CardPile.Get(PileType.Deck, p).Cards.Any((CardModel c) => c != null && c.Rarity == CardRarity.Basic && c.IsRemovable));
		}

		// Token: 0x06006355 RID: 25429 RVA: 0x0024F590 File Offset: 0x0024D790
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			IReadOnlyList<CardModel> cards = PileType.Deck.GetPile(base.Owner).Cards;
			EventOption eventOption;
			if (cards.Any((CardModel c) => ModelDb.Enchantment<Slither>().CanEnchant(c)))
			{
				eventOption = new EventOption(this, new Func<Task>(this.Snake), "WOOD_CARVINGS.pages.INITIAL.options.SNAKE", HoverTipFactory.FromEnchantment<Slither>(1));
			}
			else
			{
				eventOption = new EventOption(this, null, "WOOD_CARVINGS.pages.INITIAL.options.SNAKE_LOCKED", Array.Empty<IHoverTip>());
			}
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Bird), "WOOD_CARVINGS.pages.INITIAL.options.BIRD", HoverTipFactory.FromCardWithCardHoverTips<Peck>(false)),
				eventOption,
				new EventOption(this, new Func<Task>(this.Torus), "WOOD_CARVINGS.pages.INITIAL.options.TORUS", HoverTipFactory.FromCardWithCardHoverTips<ToricToughness>(false))
			});
		}

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06006356 RID: 25430 RVA: 0x0024F658 File Offset: 0x0024D858
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("BirdCard", ModelDb.Card<Peck>().Title),
					new StringVar("SnakeEnchantment", ModelDb.Enchantment<Slither>().Title.GetFormattedText()),
					new StringVar("ToricCard", ModelDb.Card<ToricToughness>().Title)
				});
			}
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x0024F6BC File Offset: 0x0024D8BC
		private async Task Bird()
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckGeneric(base.Owner, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1), (CardModel c) => c.IsTransformable && c.Rarity == CardRarity.Basic, null);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.TransformTo<Peck>(cardModel, CardPreviewStyle.EventLayout);
			}
			base.SetEventFinished(base.L10NLookup("WOOD_CARVINGS.pages.BIRD.description"));
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x0024F700 File Offset: 0x0024D900
		private async Task Snake()
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<Slither>(), 1, new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1));
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<Slither>(cardModel, 1m);
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
			base.SetEventFinished(base.L10NLookup("WOOD_CARVINGS.pages.SNAKE.description"));
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x0024F744 File Offset: 0x0024D944
		private async Task Torus()
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckGeneric(base.Owner, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1), (CardModel c) => c != null && c.IsTransformable && c.Rarity == CardRarity.Basic, null);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.TransformTo<ToricToughness>(cardModel, CardPreviewStyle.EventLayout);
			}
			base.SetEventFinished(base.L10NLookup("WOOD_CARVINGS.pages.TORUS.description"));
		}

		// Token: 0x04002509 RID: 9481
		private const string _birdCardKey = "BirdCard";

		// Token: 0x0400250A RID: 9482
		private const string _snakeEnchantmentKey = "SnakeEnchantment";

		// Token: 0x0400250B RID: 9483
		private const string _toricCardKey = "ToricCard";
	}
}
