using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x0200085F RID: 2143
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockBossEncounter : EncounterModel
	{
		// Token: 0x170019E0 RID: 6624
		// (get) Token: 0x06006593 RID: 26003 RVA: 0x00252909 File Offset: 0x00250B09
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x170019E1 RID: 6625
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x0025290C File Offset: 0x00250B0C
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019E2 RID: 6626
		// (get) Token: 0x06006595 RID: 26005 RVA: 0x0025290F File Offset: 0x00250B0F
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BigDummy>());
			}
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x0025291B File Offset: 0x00250B1B
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<BigDummy>().ToMutable(), null));
		}
	}
}
