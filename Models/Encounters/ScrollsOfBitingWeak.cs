using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200083E RID: 2110
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ScrollsOfBitingWeak : EncounterModel
	{
		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x002518ED File Offset: 0x0024FAED
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x060064CF RID: 25807 RVA: 0x002518F0 File Offset: 0x0024FAF0
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x060064D0 RID: 25808 RVA: 0x002518F3 File Offset: 0x0024FAF3
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Scrolls);
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x060064D1 RID: 25809 RVA: 0x002518FC File Offset: 0x0024FAFC
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<ScrollOfBiting>());
			}
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x00251908 File Offset: 0x0024FB08
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			ScrollOfBiting scrollOfBiting = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			ScrollOfBiting scrollOfBiting2 = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			ScrollOfBiting scrollOfBiting3 = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			int num = base.Rng.NextInt(3);
			scrollOfBiting.StarterMoveIdx = num;
			scrollOfBiting2.StarterMoveIdx = (num + 1) % 3;
			scrollOfBiting3.StarterMoveIdx = (num + 2) % 3;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(scrollOfBiting, null),
				new ValueTuple<MonsterModel, string>(scrollOfBiting2, null),
				new ValueTuple<MonsterModel, string>(scrollOfBiting3, null)
			});
		}
	}
}
