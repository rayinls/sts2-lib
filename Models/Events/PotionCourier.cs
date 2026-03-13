using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E0 RID: 2016
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotionCourier : EventModel
	{
		// Token: 0x060061FF RID: 25087 RVA: 0x00249148 File Offset: 0x00247348
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.GrabPotions), "POTION_COURIER.pages.INITIAL.options.GRAB_POTIONS", new IHoverTip[] { HoverTipFactory.FromPotion<FoulPotion>() }),
				new EventOption(this, new Func<Task>(this.Ransack), "POTION_COURIER.pages.INITIAL.options.RANSACK", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06006200 RID: 25088 RVA: 0x002491A7 File Offset: 0x002473A7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("FoulPotions", 3m));
			}
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x002491BE File Offset: 0x002473BE
		public override bool IsAllowed(RunState runState)
		{
			return runState.CurrentActIndex > 0;
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x002491CC File Offset: 0x002473CC
		private async Task GrabPotions()
		{
			List<Reward> list = new List<Reward>();
			for (int i = 0; i < base.DynamicVars["FoulPotions"].IntValue; i++)
			{
				list.Add(new PotionReward(ModelDb.Potion<FoulPotion>().ToMutable(), base.Owner));
			}
			await RewardsCmd.OfferCustom(base.Owner, list);
			base.SetEventFinished(base.L10NLookup("POTION_COURIER.pages.GRAB_POTIONS.description"));
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x00249210 File Offset: 0x00247410
		private async Task Ransack()
		{
			IEnumerable<PotionModel> enumerable = from p in base.Owner.Character.PotionPool.GetUnlockedPotions(base.Owner.UnlockState).Concat(ModelDb.PotionPool<SharedPotionPool>().GetUnlockedPotions(base.Owner.UnlockState))
				where p.Rarity == PotionRarity.Uncommon
				select p;
			PotionModel potionModel = base.Owner.PlayerRng.Rewards.NextItem<PotionModel>(enumerable);
			if (potionModel != null)
			{
				await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
				{
					new PotionReward(potionModel.ToMutable(), base.Owner)
				});
			}
			base.SetEventFinished(base.L10NLookup("POTION_COURIER.pages.RANSACK.description"));
		}

		// Token: 0x040024B1 RID: 9393
		private const string _foulPotionsKey = "FoulPotions";
	}
}
