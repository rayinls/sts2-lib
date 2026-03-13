using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000520 RID: 1312
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JuzuBracelet : RelicModel
	{
		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06004C6B RID: 19563 RVA: 0x0021591B File Offset: 0x00213B1B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x06004C6C RID: 19564 RVA: 0x0021591E File Offset: 0x00213B1E
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x06004C6D RID: 19565 RVA: 0x00215928 File Offset: 0x00213B28
		public override IReadOnlySet<RoomType> ModifyUnknownMapPointRoomTypes(IReadOnlySet<RoomType> roomTypes)
		{
			HashSet<RoomType> hashSet = new HashSet<RoomType>();
			foreach (RoomType roomType in roomTypes)
			{
				hashSet.Add(roomType);
			}
			HashSet<RoomType> hashSet2 = hashSet;
			hashSet2.Remove(RoomType.Monster);
			return hashSet2;
		}
	}
}
