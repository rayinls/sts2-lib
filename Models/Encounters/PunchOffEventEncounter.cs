using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200083A RID: 2106
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PunchOffEventEncounter : EncounterModel
	{
		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x060064B5 RID: 25781 RVA: 0x0025159A File Offset: 0x0024F79A
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x060064B6 RID: 25782 RVA: 0x0025159D File Offset: 0x0024F79D
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<PunchConstruct>());
			}
		}

		// Token: 0x060064B7 RID: 25783 RVA: 0x002515AC File Offset: 0x0024F7AC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			PunchConstruct punchConstruct = (PunchConstruct)ModelDb.Monster<PunchConstruct>().ToMutable();
			punchConstruct.StartsWithStrongPunch = true;
			punchConstruct.StartingHpReduction = base.Rng.NextInt(2, 10);
			PunchConstruct punchConstruct2 = (PunchConstruct)ModelDb.Monster<PunchConstruct>().ToMutable();
			punchConstruct2.StartingHpReduction = base.Rng.NextInt(2, 10);
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(punchConstruct, null),
				new ValueTuple<MonsterModel, string>(punchConstruct2, null)
			});
		}
	}
}
