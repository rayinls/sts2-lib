using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200080E RID: 2062
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConstructMenagerieNormal : EncounterModel
	{
		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x060063A7 RID: 25511 RVA: 0x0025003F File Offset: 0x0024E23F
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x060063A8 RID: 25512 RVA: 0x00250042 File Offset: 0x0024E242
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<PunchConstruct>(),
					ModelDb.Monster<CubexConstruct>()
				});
			}
		}

		// Token: 0x060063A9 RID: 25513 RVA: 0x00250060 File Offset: 0x0024E260
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<PunchConstruct>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<CubexConstruct>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<CubexConstruct>().ToMutable(), null)
			});
		}
	}
}
