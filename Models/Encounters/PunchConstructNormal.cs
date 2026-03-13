using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000839 RID: 2105
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PunchConstructNormal : EncounterModel
	{
		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x060064B1 RID: 25777 RVA: 0x0025156C File Offset: 0x0024F76C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x060064B2 RID: 25778 RVA: 0x0025156F File Offset: 0x0024F76F
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<PunchConstruct>());
			}
		}

		// Token: 0x060064B3 RID: 25779 RVA: 0x0025157B File Offset: 0x0024F77B
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<PunchConstruct>().ToMutable(), null));
		}
	}
}
