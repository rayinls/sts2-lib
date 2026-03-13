using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x02000862 RID: 2146
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockNoRewardsEncounter : EncounterModel
	{
		// Token: 0x170019E9 RID: 6633
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x0025299C File Offset: 0x00250B9C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019EA RID: 6634
		// (get) Token: 0x060065A3 RID: 26019 RVA: 0x0025299F File Offset: 0x00250B9F
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019EB RID: 6635
		// (get) Token: 0x060065A4 RID: 26020 RVA: 0x002529A2 File Offset: 0x00250BA2
		public override bool ShouldGiveRewards
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170019EC RID: 6636
		// (get) Token: 0x060065A5 RID: 26021 RVA: 0x002529A5 File Offset: 0x00250BA5
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BigDummy>());
			}
		}

		// Token: 0x060065A6 RID: 26022 RVA: 0x002529B1 File Offset: 0x00250BB1
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<BigDummy>().ToMutable(), null));
		}
	}
}
