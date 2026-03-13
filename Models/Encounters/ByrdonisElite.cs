using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200080B RID: 2059
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ByrdonisElite : EncounterModel
	{
		// Token: 0x170018B9 RID: 6329
		// (get) Token: 0x06006395 RID: 25493 RVA: 0x0024FF36 File Offset: 0x0024E136
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x170018BA RID: 6330
		// (get) Token: 0x06006396 RID: 25494 RVA: 0x0024FF39 File Offset: 0x0024E139
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Byrdonis>());
			}
		}

		// Token: 0x06006397 RID: 25495 RVA: 0x0024FF45 File Offset: 0x0024E145
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Byrdonis>().ToMutable(), null));
		}
	}
}
