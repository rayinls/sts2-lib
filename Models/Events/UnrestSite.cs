using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007FC RID: 2044
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnrestSite : EventModel
	{
		// Token: 0x0600631E RID: 25374 RVA: 0x0024E434 File Offset: 0x0024C634
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Creature.CurrentHp <= p.Creature.MaxHp * 0.70m);
		}

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x0600631F RID: 25375 RVA: 0x0024E460 File Offset: 0x0024C660
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HealVar(0m),
					new DynamicVar("MaxHpLoss", 8m)
				});
			}
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x0024E490 File Offset: 0x0024C690
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Rest), "UNREST_SITE.pages.INITIAL.options.REST", HoverTipFactory.FromCardWithCardHoverTips<PoorSleep>(false)),
				new EventOption(this, new Func<Task>(this.Kill), "UNREST_SITE.pages.INITIAL.options.KILL", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x0024E4E7 File Offset: 0x0024C6E7
		public override void CalculateVars()
		{
			base.DynamicVars.Heal.BaseValue = base.Owner.Creature.MaxHp - base.Owner.Creature.CurrentHp;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x0024E520 File Offset: 0x0024C720
		private async Task Rest()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
			await CardPileCmd.AddCursesToDeck(new <>z__ReadOnlySingleElementList<CardModel>(ModelDb.Card<PoorSleep>()), base.Owner);
			base.SetEventFinished(base.L10NLookup("UNREST_SITE.pages.REST.description"));
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x0024E564 File Offset: 0x0024C764
		private async Task Kill()
		{
			await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars["MaxHpLoss"].BaseValue, false);
			RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
			await RelicCmd.Obtain(relicModel, base.Owner, -1);
			base.SetEventFinished(base.L10NLookup("UNREST_SITE.pages.KILL.description"));
		}

		// Token: 0x040024FA RID: 9466
		private const string _maxHpLossKey = "MaxHpLoss";
	}
}
