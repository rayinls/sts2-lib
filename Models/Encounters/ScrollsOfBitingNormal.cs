using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200083D RID: 2109
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ScrollsOfBitingNormal : EncounterModel
	{
		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x060064C9 RID: 25801 RVA: 0x00251807 File Offset: 0x0024FA07
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x060064CA RID: 25802 RVA: 0x0025180A File Offset: 0x0024FA0A
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Scrolls);
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x060064CB RID: 25803 RVA: 0x00251813 File Offset: 0x0024FA13
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<ScrollOfBiting>());
			}
		}

		// Token: 0x060064CC RID: 25804 RVA: 0x00251820 File Offset: 0x0024FA20
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			ScrollOfBiting scrollOfBiting = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			ScrollOfBiting scrollOfBiting2 = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			ScrollOfBiting scrollOfBiting3 = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			ScrollOfBiting scrollOfBiting4 = (ScrollOfBiting)ModelDb.Monster<ScrollOfBiting>().ToMutable();
			int num = base.Rng.NextInt(3);
			scrollOfBiting.StarterMoveIdx = num;
			scrollOfBiting2.StarterMoveIdx = (num + 1) % 3;
			scrollOfBiting3.StarterMoveIdx = (num + 2) % 3;
			scrollOfBiting4.StarterMoveIdx = 2;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(scrollOfBiting, null),
				new ValueTuple<MonsterModel, string>(scrollOfBiting2, null),
				new ValueTuple<MonsterModel, string>(scrollOfBiting3, null),
				new ValueTuple<MonsterModel, string>(scrollOfBiting4, null)
			});
		}
	}
}
