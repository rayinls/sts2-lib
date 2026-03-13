using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007FA RID: 2042
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TrashHeap : EventModel
	{
		// Token: 0x1700188D RID: 6285
		// (get) Token: 0x06006304 RID: 25348 RVA: 0x0024DC66 File Offset: 0x0024BE66
		private static RelicModel[] Relics
		{
			get
			{
				return new RelicModel[]
				{
					ModelDb.Relic<DarkstonePeriapt>(),
					ModelDb.Relic<DreamCatcher>(),
					ModelDb.Relic<HandDrill>(),
					ModelDb.Relic<MawBank>(),
					ModelDb.Relic<TheBoot>()
				};
			}
		}

		// Token: 0x1700188E RID: 6286
		// (get) Token: 0x06006305 RID: 25349 RVA: 0x0024DC98 File Offset: 0x0024BE98
		private static CardModel[] Cards
		{
			get
			{
				return new CardModel[]
				{
					ModelDb.Card<Caltrops>(),
					ModelDb.Card<Clash>(),
					ModelDb.Card<Distraction>(),
					ModelDb.Card<DualWield>(),
					ModelDb.Card<Entrench>(),
					ModelDb.Card<HelloWorld>(),
					ModelDb.Card<Outmaneuver>(),
					ModelDb.Card<Rebound>(),
					ModelDb.Card<RipAndTear>(),
					ModelDb.Card<Stack>()
				};
			}
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x0024DCFD File Offset: 0x0024BEFD
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player player) => player.Creature.CurrentHp > 5);
		}

		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06006307 RID: 25351 RVA: 0x0024DD29 File Offset: 0x0024BF29
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar(8m),
					new GoldVar(100)
				});
			}
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x0024DD50 File Offset: 0x0024BF50
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.DiveIn), "TRASH_HEAP.pages.INITIAL.options.DIVE_IN", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars.HpLoss.IntValue),
				new EventOption(this, new Func<Task>(this.Grab), "TRASH_HEAP.pages.INITIAL.options.GRAB", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x0024DDC0 File Offset: 0x0024BFC0
		private async Task DiveIn()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.HpLoss.IntValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			RelicModel relicModel = base.Rng.NextItem<RelicModel>(TrashHeap.Relics);
			await RelicCmd.Obtain(relicModel.ToMutable(), base.Owner, -1);
			base.SetEventFinished(base.L10NLookup("TRASH_HEAP.pages.DIVE_IN.description"));
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x0024DE04 File Offset: 0x0024C004
		private async Task Grab()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
			CardModel cardModel = base.Owner.RunState.CreateCard(base.Rng.NextItem<CardModel>(TrashHeap.Cards), base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("TRASH_HEAP.pages.GRAB.description"));
		}
	}
}
