using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000820 RID: 2080
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FrogKnightNormal : EncounterModel
	{
		// Token: 0x17001907 RID: 6407
		// (get) Token: 0x06006418 RID: 25624 RVA: 0x00250AEB File Offset: 0x0024ECEB
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001908 RID: 6408
		// (get) Token: 0x06006419 RID: 25625 RVA: 0x00250AEE File Offset: 0x0024ECEE
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<FrogKnight>());
			}
		}

		// Token: 0x0600641A RID: 25626 RVA: 0x00250AFA File Offset: 0x0024ECFA
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<FrogKnight>().ToMutable(), null));
		}
	}
}
