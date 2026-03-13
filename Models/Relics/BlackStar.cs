using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004BA RID: 1210
	public sealed class BlackStar : RelicModel
	{
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060049FF RID: 18943 RVA: 0x002111DF File Offset: 0x0020F3DF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004A00 RID: 18944 RVA: 0x002111E2 File Offset: 0x0020F3E2
		[NullableContext(1)]
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
			rewards.Add(new RelicReward(player));
			return true;
		}
	}
}
