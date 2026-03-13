using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005C3 RID: 1475
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WhiteBeastStatue : RelicModel
	{
		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x060050A4 RID: 20644 RVA: 0x0021D6AB File Offset: 0x0021B8AB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x060050A5 RID: 20645 RVA: 0x0021D6AE File Offset: 0x0021B8AE
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x060050A6 RID: 20646 RVA: 0x0021D6B6 File Offset: 0x0021B8B6
		public override bool ShouldForcePotionReward(Player player, RoomType roomType)
		{
			return player == base.Owner && roomType.IsCombatRoom();
		}
	}
}
