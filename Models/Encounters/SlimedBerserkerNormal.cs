using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000843 RID: 2115
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlimedBerserkerNormal : EncounterModel
	{
		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x060064E8 RID: 25832 RVA: 0x00251A7B File Offset: 0x0024FC7B
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x060064E9 RID: 25833 RVA: 0x00251A7E File Offset: 0x0024FC7E
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SlimedBerserker>());
			}
		}

		// Token: 0x060064EA RID: 25834 RVA: 0x00251A8A File Offset: 0x0024FC8A
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SlimedBerserker>().ToMutable(), null));
		}
	}
}
