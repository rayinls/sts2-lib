using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Rewards;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x02000801 RID: 2049
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Wellspring : EventModel
	{
		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06006347 RID: 25415 RVA: 0x0024F2A3 File Offset: 0x0024D4A3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("BatheCurses", 1m));
			}
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x0024F2BC File Offset: 0x0024D4BC
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Bottle), "WELLSPRING.pages.INITIAL.options.BOTTLE", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Bathe), "WELLSPRING.pages.INITIAL.options.BATHE", HoverTipFactory.FromCardWithCardHoverTips<Guilty>(false))
			});
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x0024F314 File Offset: 0x0024D514
		private async Task Bottle()
		{
			IEnumerable<PotionModel> enumerable = base.Owner.Character.PotionPool.GetUnlockedPotions(base.Owner.UnlockState).Concat(ModelDb.PotionPool<SharedPotionPool>().GetUnlockedPotions(base.Owner.UnlockState));
			PotionModel potionModel = base.Owner.PlayerRng.Rewards.NextItem<PotionModel>(enumerable);
			if (potionModel != null)
			{
				await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
				{
					new PotionReward(potionModel.ToMutable(), base.Owner)
				});
			}
			base.SetEventFinished(base.L10NLookup("WELLSPRING.pages.BOTTLE.description"));
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x0024F358 File Offset: 0x0024D558
		private async Task Bathe()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			await this.AddGuilty(base.DynamicVars["BatheCurses"].IntValue);
			base.SetEventFinished(base.L10NLookup("WELLSPRING.pages.BATHE.description"));
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x0024F39C File Offset: 0x0024D59C
		private async Task AddGuilty(int amount)
		{
			await CardPileCmd.AddCursesToDeck(Enumerable.Repeat<Guilty>(ModelDb.Card<Guilty>(), amount), base.Owner);
		}

		// Token: 0x04002508 RID: 9480
		private const string _batheKey = "BatheCurses";
	}
}
