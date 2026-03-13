using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x02000864 RID: 2148
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockTwoMonsterEncounter : EncounterModel
	{
		// Token: 0x170019F1 RID: 6641
		// (get) Token: 0x060065AF RID: 26031 RVA: 0x00252A40 File Offset: 0x00250C40
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019F2 RID: 6642
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x00252A43 File Offset: 0x00250C43
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019F3 RID: 6643
		// (get) Token: 0x060065B1 RID: 26033 RVA: 0x00252A46 File Offset: 0x00250C46
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BigDummy>());
			}
		}

		// Token: 0x060065B2 RID: 26034 RVA: 0x00252A52 File Offset: 0x00250C52
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<BigDummy>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<BigDummy>().ToMutable(), null)
			});
		}
	}
}
