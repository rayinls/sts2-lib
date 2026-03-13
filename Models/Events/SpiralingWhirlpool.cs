using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007EA RID: 2026
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpiralingWhirlpool : EventModel
	{
		// Token: 0x17001860 RID: 6240
		// (get) Token: 0x06006262 RID: 25186 RVA: 0x0024A9BA File Offset: 0x00248BBA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(0m));
			}
		}

		// Token: 0x06006263 RID: 25187 RVA: 0x0024A9CC File Offset: 0x00248BCC
		public override void CalculateVars()
		{
			base.DynamicVars.Heal.BaseValue = ((base.Owner != null) ? (base.Owner.Creature.MaxHp * 0.33m) : 0m);
		}

		// Token: 0x06006264 RID: 25188 RVA: 0x0024AA1D File Offset: 0x00248C1D
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Deck.Cards.Any(new Func<CardModel, bool>(ModelDb.Enchantment<Spiral>().CanEnchant)));
		}

		// Token: 0x06006265 RID: 25189 RVA: 0x0024AA4C File Offset: 0x00248C4C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.ObserveTheSpiral), "SPIRALING_WHIRLPOOL.pages.INITIAL.options.OBSERVE", HoverTipFactory.FromEnchantment<Spiral>(1)),
				new EventOption(this, new Func<Task>(this.Drink), "SPIRALING_WHIRLPOOL.pages.INITIAL.options.DRINK", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x06006266 RID: 25190 RVA: 0x0024AAA4 File Offset: 0x00248CA4
		private async Task ObserveTheSpiral()
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<Spiral>(), 1, (CardModel c) => ModelDb.Enchantment<Spiral>().CanEnchant(c), new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1));
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<Spiral>(cardModel, 1m);
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
			base.SetEventFinished(base.L10NLookup("SPIRALING_WHIRLPOOL.pages.OBSERVE.description"));
		}

		// Token: 0x06006267 RID: 25191 RVA: 0x0024AAE8 File Offset: 0x00248CE8
		private async Task Drink()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.IntValue, true);
			base.SetEventFinished(base.L10NLookup("SPIRALING_WHIRLPOOL.pages.DRINK.description"));
		}
	}
}
