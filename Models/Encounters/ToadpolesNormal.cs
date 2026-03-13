using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000855 RID: 2133
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToadpolesNormal : EncounterModel
	{
		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06006555 RID: 25941 RVA: 0x00252429 File Offset: 0x00250629
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x0025242C File Offset: 0x0025062C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<Toadpole>(),
					ModelDb.Monster<CalcifiedCultist>()
				});
			}
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00252449 File Offset: 0x00250649
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<CalcifiedCultist>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>((Toadpole)ModelDb.Monster<Toadpole>().ToMutable(), null)
			});
		}
	}
}
