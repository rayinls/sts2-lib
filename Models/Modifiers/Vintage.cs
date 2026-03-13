using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007BC RID: 1980
	public class Vintage : ModifierModel
	{
		// Token: 0x060060F3 RID: 24819 RVA: 0x00243E9C File Offset: 0x0024209C
		[NullableContext(1)]
		public override bool TryModifyRewardsLate(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			CombatRoom combatRoom = room as CombatRoom;
			if (combatRoom == null)
			{
				return false;
			}
			if (combatRoom.Encounter.RoomType != RoomType.Monster)
			{
				return false;
			}
			for (int i = 0; i < rewards.Count; i++)
			{
				if (rewards[i] is CardReward)
				{
					rewards.RemoveAt(i);
					RelicReward relicReward = new RelicReward(player);
					rewards.Insert(i, relicReward);
				}
			}
			return true;
		}
	}
}
