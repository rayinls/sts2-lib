using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200056D RID: 1389
	public sealed class PrayerWheel : RelicModel
	{
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x0021960F File Offset: 0x0021780F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x00219612 File Offset: 0x00217812
		[NullableContext(1)]
		public override bool TryModifyRewards(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (room == null || room.RoomType != RoomType.Monster)
			{
				return false;
			}
			rewards.Add(new CardReward(CardCreationOptions.ForRoom(player, RoomType.Monster), 3, player));
			return true;
		}
	}
}
