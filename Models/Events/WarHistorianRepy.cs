using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007FE RID: 2046
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WarHistorianRepy : EventModel
	{
		// Token: 0x0600632E RID: 25390 RVA: 0x0024EAF5 File Offset: 0x0024CCF5
		public override bool IsAllowed(RunState runState)
		{
			return false;
		}

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x0600632F RID: 25391 RVA: 0x0024EAF8 File Offset: 0x0024CCF8
		public override bool IsShared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x0024EAFC File Offset: 0x0024CCFC
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.UnlockCage), "WAR_HISTORIAN_REPY.pages.INITIAL.options.UNLOCK_CAGE", HoverTipFactory.FromRelic<HistoryCourse>().Concat(new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<LanternKey>(false)))),
				new EventOption(this, new Func<Task>(this.UnlockChest), "WAR_HISTORIAN_REPY.pages.INITIAL.options.UNLOCK_CHEST", new IHoverTip[] { HoverTipFactory.FromCard<LanternKey>(false) })
			});
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x0024EB6C File Offset: 0x0024CD6C
		private async Task UnlockCage()
		{
			base.SetEventFinished(base.L10NLookup("WAR_HISTORIAN_REPY.pages.UNLOCK_CAGE.description"));
			base.Owner.RunState.ExtraFields.FreedRepy = true;
			await this.RemoveLanternKey();
			await RelicCmd.Obtain(ModelDb.Relic<HistoryCourse>().ToMutable(), base.Owner, -1);
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x0024EBB0 File Offset: 0x0024CDB0
		private async Task UnlockChest()
		{
			base.SetEventFinished(base.L10NLookup("WAR_HISTORIAN_REPY.pages.UNLOCK_CHEST.description"));
			await this.RemoveLanternKey();
			List<Reward> list = new List<Reward>();
			list.Add(new PotionReward(base.Owner));
			list.Add(new PotionReward(base.Owner));
			list.Add(new RelicReward(base.Owner));
			list.Add(new RelicReward(base.Owner));
			await RewardsCmd.OfferCustom(base.Owner, list);
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x0024EBF4 File Offset: 0x0024CDF4
		private async Task RemoveLanternKey()
		{
			List<CardModel> list = base.Owner.Deck.Cards.Where((CardModel c) => c is LanternKey).ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				PlayerCmd.CompleteQuest(cardModel);
				await CardPileCmd.RemoveFromDeck(cardModel, true);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}
