using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000808 RID: 2056
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlbugsNormal : EncounterModel
	{
		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06006380 RID: 25472 RVA: 0x0024FCF0 File Offset: 0x0024DEF0
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Workers);
			}
		}

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06006381 RID: 25473 RVA: 0x0024FCF8 File Offset: 0x0024DEF8
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06006382 RID: 25474 RVA: 0x0024FCFB File Offset: 0x0024DEFB
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06006383 RID: 25475 RVA: 0x0024FCFE File Offset: 0x0024DEFE
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return BowlbugsNormal._workerValidCounts.Keys;
			}
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x0024FD0C File Offset: 0x0024DF0C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected unsafe override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			List<MonsterModel> currentWorkers = new List<MonsterModel>();
			int num = 1;
			List<ValueTuple<MonsterModel, string>> list = new List<ValueTuple<MonsterModel, string>>(num);
			CollectionsMarshal.SetCount<ValueTuple<MonsterModel, string>>(list, num);
			Span<ValueTuple<MonsterModel, string>> span = CollectionsMarshal.AsSpan<ValueTuple<MonsterModel, string>>(list);
			int num2 = 0;
			*span[num2] = new ValueTuple<MonsterModel, string>(ModelDb.Monster<BowlbugRock>().ToMutable(), BowlbugsNormal._slotNames[0]);
			List<ValueTuple<MonsterModel, string>> list2 = list;
			Func<MonsterModel, bool> <>9__0;
			for (int i = 0; i < 2; i++)
			{
				IEnumerable<MonsterModel> keys = BowlbugsNormal._workerValidCounts.Keys;
				Func<MonsterModel, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (MonsterModel r) => currentWorkers.Count((MonsterModel c) => c == r) < BowlbugsNormal._workerValidCounts[r]);
				}
				List<MonsterModel> list3 = keys.Where(func).ToList<MonsterModel>();
				MonsterModel monsterModel = base.Rng.NextItem<MonsterModel>(list3);
				currentWorkers.Add(monsterModel);
				list2.Add(new ValueTuple<MonsterModel, string>(monsterModel.ToMutable(), BowlbugsNormal._slotNames[i + 1]));
			}
			return list2;
		}

		// Token: 0x04002518 RID: 9496
		private static readonly Dictionary<MonsterModel, int> _workerValidCounts = new Dictionary<MonsterModel, int>
		{
			{
				ModelDb.Monster<BowlbugEgg>(),
				1
			},
			{
				ModelDb.Monster<BowlbugSilk>(),
				1
			},
			{
				ModelDb.Monster<BowlbugNectar>(),
				1
			}
		};

		// Token: 0x04002519 RID: 9497
		private static readonly string[] _slotNames = new string[] { "first", "middle", "last" };
	}
}
