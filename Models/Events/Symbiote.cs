using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007EF RID: 2031
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Symbiote : EventModel
	{
		// Token: 0x06006284 RID: 25220 RVA: 0x0024B23B File Offset: 0x0024943B
		public override bool IsAllowed(RunState runState)
		{
			return runState.CurrentActIndex > 0;
		}

		// Token: 0x06006285 RID: 25221 RVA: 0x0024B248 File Offset: 0x00249448
		protected unsafe override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			CardPile pile = PileType.Deck.GetPile(base.Owner);
			IEnumerable<CardModel> cards = pile.Cards;
			Func<CardModel, bool> func;
			if ((func = Symbiote.<>O.<0>__CanEnchant) == null)
			{
				func = (Symbiote.<>O.<0>__CanEnchant = new Func<CardModel, bool>(Symbiote.CanEnchant));
			}
			EventOption eventOption;
			if (cards.Any(func))
			{
				eventOption = new EventOption(this, new Func<Task>(this.Approach), "SYMBIOTE.pages.INITIAL.options.APPROACH", HoverTipFactory.FromEnchantment<Corrupted>(1));
			}
			else
			{
				eventOption = new EventOption(this, null, "SYMBIOTE.pages.INITIAL.options.APPROACH_LOCKED", Array.Empty<IHoverTip>());
			}
			EventOption eventOption2 = new EventOption(this, new Func<Task>(this.KillWithFire), "SYMBIOTE.pages.INITIAL.options.KILL_WITH_FIRE", new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>()) });
			int num = 2;
			List<EventOption> list = new List<EventOption>(num);
			CollectionsMarshal.SetCount<EventOption>(list, num);
			Span<EventOption> span = CollectionsMarshal.AsSpan<EventOption>(list);
			int num2 = 0;
			*span[num2] = eventOption;
			num2++;
			*span[num2] = eventOption2;
			return list;
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06006286 RID: 25222 RVA: 0x0024B323 File Offset: 0x00249523
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Enchantment", ModelDb.Enchantment<Corrupted>().Title.GetFormattedText()),
					new CardsVar(1)
				});
			}
		}

		// Token: 0x06006287 RID: 25223 RVA: 0x0024B358 File Offset: 0x00249558
		private async Task Approach()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<Corrupted>(), 1, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<Corrupted>(cardModel, 1m);
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
			base.SetEventFinished(base.L10NLookup("SYMBIOTE.pages.APPROACH.description"));
		}

		// Token: 0x06006288 RID: 25224 RVA: 0x0024B39C File Offset: 0x0024959C
		private async Task KillWithFire()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				await CardCmd.TransformToRandom(cardModel, base.Rng, CardPreviewStyle.EventLayout);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			base.SetEventFinished(base.L10NLookup("SYMBIOTE.pages.KILL_WITH_FIRE.description"));
		}

		// Token: 0x06006289 RID: 25225 RVA: 0x0024B3DF File Offset: 0x002495DF
		private static bool CanEnchant(CardModel card)
		{
			return ModelDb.Enchantment<Corrupted>().CanEnchant(card);
		}

		// Token: 0x040024D8 RID: 9432
		private const string _enchantmentKey = "Enchantment";

		// Token: 0x02001D7E RID: 7550
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400767A RID: 30330
			[Nullable(0)]
			public static Func<CardModel, bool> <0>__CanEnchant;
		}
	}
}
