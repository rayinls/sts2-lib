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
	// Token: 0x020005C4 RID: 1476
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WhiteStar : RelicModel
	{
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x060050A8 RID: 20648 RVA: 0x0021D6D6 File Offset: 0x0021B8D6
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x060050A9 RID: 20649 RVA: 0x0021D6D9 File Offset: 0x0021B8D9
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x060050AA RID: 20650 RVA: 0x0021D6E1 File Offset: 0x0021B8E1
		public override bool TryModifyRewards(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (room == null || room.RoomType != RoomType.Elite)
			{
				return false;
			}
			rewards.Add(new CardReward(CardCreationOptions.ForRoom(base.Owner, RoomType.Boss), 3, player));
			return true;
		}
	}
}
