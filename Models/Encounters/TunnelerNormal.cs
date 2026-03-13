using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000857 RID: 2135
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TunnelerNormal : EncounterModel
	{
		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x0025250E File Offset: 0x0025070E
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterTag>(new EncounterTag[]
				{
					EncounterTag.Burrower,
					EncounterTag.Workers
				});
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x0600655F RID: 25951 RVA: 0x00252523 File Offset: 0x00250723
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019C3 RID: 6595
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x00252528 File Offset: 0x00250728
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				MonsterModel monsterModel = ModelDb.Monster<Tunneler>();
				MonsterModel[] bugs = TunnelerNormal._bugs;
				int num = 0;
				MonsterModel[] array = new MonsterModel[1 + bugs.Length];
				array[num] = monsterModel;
				num++;
				ReadOnlySpan<MonsterModel> readOnlySpan = new ReadOnlySpan<MonsterModel>(bugs);
				readOnlySpan.CopyTo(new Span<MonsterModel>(array).Slice(num, readOnlySpan.Length));
				num += readOnlySpan.Length;
				return new <>z__ReadOnlyArray<MonsterModel>(array);
			}
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x0025258C File Offset: 0x0025078C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(base.Rng.NextItem<MonsterModel>(TunnelerNormal._bugs).ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Tunneler>().ToMutable(), null)
			});
		}

		// Token: 0x04002545 RID: 9541
		private static readonly MonsterModel[] _bugs = new MonsterModel[]
		{
			ModelDb.Monster<BowlbugEgg>(),
			ModelDb.Monster<BowlbugSilk>()
		};
	}
}
