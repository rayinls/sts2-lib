using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000842 RID: 2114
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SkulkingColonyElite : EncounterModel
	{
		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x060064E4 RID: 25828 RVA: 0x00251A4D File Offset: 0x0024FC4D
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x060064E5 RID: 25829 RVA: 0x00251A50 File Offset: 0x0024FC50
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SkulkingColony>());
			}
		}

		// Token: 0x060064E6 RID: 25830 RVA: 0x00251A5C File Offset: 0x0024FC5C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SkulkingColony>().ToMutable(), null));
		}
	}
}
