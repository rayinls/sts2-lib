using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004EB RID: 1259
	public class Driftwood : RelicModel
	{
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06004B13 RID: 19219 RVA: 0x00213258 File Offset: 0x00211458
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x0021325C File Offset: 0x0021145C
		[NullableContext(1)]
		public override bool TryModifyRewardsLate(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			foreach (CardReward cardReward in rewards.OfType<CardReward>())
			{
				cardReward.CanReroll = true;
			}
			return true;
		}
	}
}
