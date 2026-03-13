using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B6 RID: 1974
	[NullableContext(1)]
	[Nullable(0)]
	public class Midas : ModifierModel
	{
		// Token: 0x060060E0 RID: 24800 RVA: 0x00243B08 File Offset: 0x00241D08
		public override bool TryModifyRewardsLate(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			List<Reward> list = new List<Reward>();
			foreach (Reward reward in rewards)
			{
				GoldReward goldReward = reward as GoldReward;
				if (goldReward != null)
				{
					list.Add(new GoldReward(goldReward.Amount * 2, player, false));
				}
				else
				{
					list.Add(reward);
				}
			}
			rewards.Clear();
			rewards.AddRange(list);
			return true;
		}

		// Token: 0x060060E1 RID: 24801 RVA: 0x00243B8C File Offset: 0x00241D8C
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			List<SmithRestSiteOption> list = options.OfType<SmithRestSiteOption>().ToList<SmithRestSiteOption>();
			foreach (SmithRestSiteOption smithRestSiteOption in list)
			{
				options.Remove(smithRestSiteOption);
			}
			return true;
		}
	}
}
