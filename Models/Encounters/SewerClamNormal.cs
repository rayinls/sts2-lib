using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000840 RID: 2112
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SewerClamNormal : EncounterModel
	{
		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x060064DA RID: 25818 RVA: 0x002519E6 File Offset: 0x0024FBE6
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x002519E9 File Offset: 0x0024FBE9
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SewerClam>());
			}
		}

		// Token: 0x060064DC RID: 25820 RVA: 0x002519F5 File Offset: 0x0024FBF5
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SewerClam>().ToMutable(), null));
		}
	}
}
