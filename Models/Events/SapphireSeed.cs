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
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E7 RID: 2023
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SapphireSeed : EventModel
	{
		// Token: 0x06006243 RID: 25155 RVA: 0x0024A1A8 File Offset: 0x002483A8
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Eat), "SAPPHIRE_SEED.pages.INITIAL.options.EAT", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Plant), "SAPPHIRE_SEED.pages.INITIAL.options.PLANT", HoverTipFactory.FromEnchantment<Sown>(1))
			});
		}

		// Token: 0x1700185A RID: 6234
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x0024A1FF File Offset: 0x002483FF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("Enchantment", ModelDb.Enchantment<Sown>().Title.GetFormattedText()),
					new HealVar(9m)
				});
			}
		}

		// Token: 0x06006245 RID: 25157 RVA: 0x0024A238 File Offset: 0x00248438
		private async Task Eat()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.IntValue, true);
			CardModel cardModel = (await CardSelectCmd.FromDeckForUpgrade(base.Owner, new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, 1))).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			base.SetEventFinished(base.L10NLookup("SAPPHIRE_SEED.pages.EAT.description"));
		}

		// Token: 0x06006246 RID: 25158 RVA: 0x0024A27C File Offset: 0x0024847C
		private async Task Plant()
		{
			EnchantmentModel sown = ModelDb.Enchantment<Sown>();
			List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => sown.CanEnchant(c)).ToList<CardModel>();
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(list, sown, 1, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<Sown>(cardModel, 1m);
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
			base.SetEventFinished(base.L10NLookup("SAPPHIRE_SEED.pages.PLANT.description"));
		}

		// Token: 0x040024BF RID: 9407
		private const string _enchantmentKey = "Enchantment";
	}
}
