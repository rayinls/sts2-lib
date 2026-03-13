using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000809 RID: 2057
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BowlbugsWeak : EncounterModel
	{
		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06006387 RID: 25479 RVA: 0x0024FE4E File Offset: 0x0024E04E
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06006388 RID: 25480 RVA: 0x0024FE51 File Offset: 0x0024E051
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Workers);
			}
		}

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06006389 RID: 25481 RVA: 0x0024FE59 File Offset: 0x0024E059
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x0600638A RID: 25482 RVA: 0x0024FE5C File Offset: 0x0024E05C
		private static MonsterModel[] Bugs
		{
			get
			{
				return new MonsterModel[]
				{
					ModelDb.Monster<BowlbugEgg>(),
					ModelDb.Monster<BowlbugNectar>()
				};
			}
		}

		// Token: 0x170018B5 RID: 6325
		// (get) Token: 0x0600638B RID: 25483 RVA: 0x0024FE74 File Offset: 0x0024E074
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return BowlbugsWeak.Bugs.Concat(new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BowlbugRock>()));
			}
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x0024FE8C File Offset: 0x0024E08C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<BowlbugRock>().ToMutable(), "odd"),
				new ValueTuple<MonsterModel, string>(base.Rng.NextItem<MonsterModel>(BowlbugsWeak.Bugs).ToMutable(), "even")
			});
		}

		// Token: 0x170018B6 RID: 6326
		// (get) Token: 0x0600638D RID: 25485 RVA: 0x0024FEE5 File Offset: 0x0024E0E5
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}
	}
}
