using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x02000861 RID: 2145
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockMonsterEncounter : EncounterModel
	{
		// Token: 0x170019E6 RID: 6630
		// (get) Token: 0x0600659D RID: 26013 RVA: 0x0025296B File Offset: 0x00250B6B
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019E7 RID: 6631
		// (get) Token: 0x0600659E RID: 26014 RVA: 0x0025296E File Offset: 0x00250B6E
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019E8 RID: 6632
		// (get) Token: 0x0600659F RID: 26015 RVA: 0x00252971 File Offset: 0x00250B71
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BigDummy>());
			}
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x0025297D File Offset: 0x00250B7D
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<BigDummy>().ToMutable(), null));
		}
	}
}
