using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x02000800 RID: 2048
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WelcomeToWongos : EventModel
	{
		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x0600633C RID: 25404 RVA: 0x0024EE83 File Offset: 0x0024D083
		// (set) Token: 0x0600633D RID: 25405 RVA: 0x0024EE8B File Offset: 0x0024D08B
		[Nullable(2)]
		private RelicModel FeaturedItem
		{
			[NullableContext(2)]
			get
			{
				return this._featuredItem;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._featuredItem = value;
			}
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x0024EE9A File Offset: 0x0024D09A
		public override bool IsAllowed(RunState runState)
		{
			if (runState.CurrentActIndex == 1)
			{
				return runState.Players.All((Player p) => p.Gold >= 100);
			}
			return false;
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x0600633F RID: 25407 RVA: 0x0024EED4 File Offset: 0x0024D0D4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("BargainBinCost", 100m),
					new DynamicVar("MysteryBoxCost", 300m),
					new DynamicVar("FeaturedItemCost", 200m),
					new DynamicVar("MysteryBoxRelicCount", 3m),
					new DynamicVar("MysteryBoxCombatCount", 5m),
					new DynamicVar("WongoPointAmount", 0m),
					new DynamicVar("RemainingWongoPointAmount", 0m),
					new DynamicVar("TotalWongoBadgeAmount", 0m),
					new StringVar("RandomRelic", "")
				});
			}
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0024EFA0 File Offset: 0x0024D1A0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			Player owner = base.Owner;
			this.FeaturedItem = RelicFactory.PullNextRelicFromFront(owner, RelicRarity.Rare);
			((StringVar)base.DynamicVars["RandomRelic"]).StringValue = this.FeaturedItem.Title.GetFormattedText();
			List<EventOption> list = new List<EventOption>();
			if (owner.Gold >= base.DynamicVars["BargainBinCost"].BaseValue)
			{
				list.Add(new EventOption(this, new Func<Task>(this.BuyBargainBin), "WELCOME_TO_WONGOS.pages.INITIAL.options.BARGAIN_BIN", Array.Empty<IHoverTip>()));
			}
			else
			{
				list.Add(new EventOption(this, null, "WELCOME_TO_WONGOS.pages.INITIAL.options.BARGAIN_BIN_LOCKED", Array.Empty<IHoverTip>()));
			}
			if (owner.Gold >= base.DynamicVars["FeaturedItemCost"].BaseValue)
			{
				list.Add(new EventOption(this, new Func<Task>(this.BuyFeaturedItem), "WELCOME_TO_WONGOS.pages.INITIAL.options.FEATURED_ITEM", this.FeaturedItem.HoverTips));
			}
			else
			{
				list.Add(new EventOption(this, null, "WELCOME_TO_WONGOS.pages.INITIAL.options.FEATURED_ITEM_LOCKED", Array.Empty<IHoverTip>()));
			}
			if (owner.Gold >= base.DynamicVars["MysteryBoxCost"].BaseValue)
			{
				list.Add(new EventOption(this, new Func<Task>(this.BuyMysteryBox), "WELCOME_TO_WONGOS.pages.INITIAL.options.MYSTERY_BOX", Array.Empty<IHoverTip>()));
			}
			else
			{
				list.Add(new EventOption(this, null, "WELCOME_TO_WONGOS.pages.INITIAL.options.MYSTERY_BOX_LOCKED", Array.Empty<IHoverTip>()));
			}
			list.Add(new EventOption(this, new Func<Task>(this.Leave), "WELCOME_TO_WONGOS.pages.INITIAL.options.LEAVE", Array.Empty<IHoverTip>()));
			return list;
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x0024F140 File Offset: 0x0024D340
		private async Task<LocString> CheckObtainWongoBadge(int pointsEarned)
		{
			int wongoPoints = SaveManager.Instance.Progress.WongoPoints;
			int num = wongoPoints % 2000;
			int num2 = num + pointsEarned;
			int num3 = wongoPoints + pointsEarned;
			base.DynamicVars["WongoPointAmount"].BaseValue = num2;
			base.DynamicVars["RemainingWongoPointAmount"].BaseValue = 2000 - num2;
			base.DynamicVars["TotalWongoBadgeAmount"].BaseValue = num3 / 2000;
			base.Owner.ExtraFields.WongoPoints = pointsEarned;
			LocString locString;
			if (num2 >= 2000)
			{
				await RelicCmd.Obtain<WongoCustomerAppreciationBadge>(base.Owner);
				locString = base.L10NLookup("WELCOME_TO_WONGOS.pages.AFTER_BUY_RECEIVE_BADGE.description");
			}
			else if (base.DynamicVars["TotalWongoBadgeAmount"].BaseValue > 0m)
			{
				locString = base.L10NLookup("WELCOME_TO_WONGOS.pages.AFTER_BUY_BADGE_COUNTER.description");
			}
			else
			{
				locString = base.L10NLookup("WELCOME_TO_WONGOS.pages.AFTER_BUY.description");
			}
			return locString;
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x0024F18C File Offset: 0x0024D38C
		private async Task BuyBargainBin()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["BargainBinCost"].BaseValue, base.Owner, GoldLossType.Spent);
			RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner, RelicRarity.Common).ToMutable();
			await RelicCmd.Obtain(relicModel, base.Owner, -1);
			LocString locString = await this.CheckObtainWongoBadge(32);
			base.SetEventFinished(locString);
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x0024F1D0 File Offset: 0x0024D3D0
		private async Task BuyMysteryBox()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["MysteryBoxCost"].BaseValue, base.Owner, GoldLossType.Spent);
			await RelicCmd.Obtain<WongosMysteryTicket>(base.Owner);
			LocString locString = await this.CheckObtainWongoBadge(8);
			base.SetEventFinished(locString);
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x0024F214 File Offset: 0x0024D414
		private async Task BuyFeaturedItem()
		{
			await PlayerCmd.LoseGold(base.DynamicVars["FeaturedItemCost"].BaseValue, base.Owner, GoldLossType.Spent);
			await RelicCmd.Obtain(this.FeaturedItem.ToMutable(), base.Owner, -1);
			LocString locString = await this.CheckObtainWongoBadge(16);
			base.SetEventFinished(locString);
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x0024F258 File Offset: 0x0024D458
		private async Task Leave()
		{
			Player owner = base.Owner;
			CardModel cardModel = base.Rng.NextItem<CardModel>(owner.Deck.Cards.Where((CardModel c) => c.IsUpgraded));
			if (cardModel != null)
			{
				CardCmd.Downgrade(cardModel);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
				await Cmd.CustomScaledWait(0.5f, 1.2f, false, default(CancellationToken));
			}
			base.SetEventFinished(base.L10NLookup("WELCOME_TO_WONGOS.pages.LEAVE.description"));
		}

		// Token: 0x040024FD RID: 9469
		private const int _wongoPointsForBadge = 2000;

		// Token: 0x040024FE RID: 9470
		private const string _bargainBinCostKey = "BargainBinCost";

		// Token: 0x040024FF RID: 9471
		private const string _featuredItemCostKey = "FeaturedItemCost";

		// Token: 0x04002500 RID: 9472
		private const string _mysteryBoxCostKey = "MysteryBoxCost";

		// Token: 0x04002501 RID: 9473
		private const string _mysteryBoxRelicCountKey = "MysteryBoxRelicCount";

		// Token: 0x04002502 RID: 9474
		private const string _mysteryBoxCombatCountKey = "MysteryBoxCombatCount";

		// Token: 0x04002503 RID: 9475
		private const string _wongoPointAmountKey = "WongoPointAmount";

		// Token: 0x04002504 RID: 9476
		private const string _remainingWongoPointAmountKey = "RemainingWongoPointAmount";

		// Token: 0x04002505 RID: 9477
		private const string _totalWongoBadgeAmountKey = "TotalWongoBadgeAmount";

		// Token: 0x04002506 RID: 9478
		private const string _randomRelicKey = "RandomRelic";

		// Token: 0x04002507 RID: 9479
		[Nullable(2)]
		private RelicModel _featuredItem;
	}
}
