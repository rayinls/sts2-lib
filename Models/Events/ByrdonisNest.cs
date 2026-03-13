using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C7 RID: 1991
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ByrdonisNest : EventModel
	{
		// Token: 0x06006125 RID: 24869 RVA: 0x002449F0 File Offset: 0x00242BF0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Eat), "BYRDONIS_NEST.pages.INITIAL.options.EAT", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Take), "BYRDONIS_NEST.pages.INITIAL.options.TAKE", HoverTipFactory.FromCardWithCardHoverTips<ByrdonisEgg>(false))
			});
		}

		// Token: 0x06006126 RID: 24870 RVA: 0x00244A47 File Offset: 0x00242C47
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => !p.HasEventPet());
		}

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x06006127 RID: 24871 RVA: 0x00244A73 File Offset: 0x00242C73
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new MaxHpVar(7m),
					new StringVar("Card", ModelDb.Card<ByrdonisEgg>().Title)
				});
			}
		}

		// Token: 0x06006128 RID: 24872 RVA: 0x00244AA8 File Offset: 0x00242CA8
		private async Task Eat()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
			base.SetEventFinished(base.L10NLookup("BYRDONIS_NEST.pages.EAT.description"));
		}

		// Token: 0x06006129 RID: 24873 RVA: 0x00244AEC File Offset: 0x00242CEC
		private async Task Take()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<ByrdonisEgg>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(cardPileAddResult2, 2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("BYRDONIS_NEST.pages.TAKE.description"));
		}

		// Token: 0x04002474 RID: 9332
		private const string _cardKey = "Card";
	}
}
