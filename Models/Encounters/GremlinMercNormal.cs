using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000823 RID: 2083
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GremlinMercNormal : EncounterModel
	{
		// Token: 0x17001910 RID: 6416
		// (get) Token: 0x06006427 RID: 25639 RVA: 0x00250B91 File Offset: 0x0024ED91
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001911 RID: 6417
		// (get) Token: 0x06006428 RID: 25640 RVA: 0x00250B94 File Offset: 0x0024ED94
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x00250B97 File Offset: 0x0024ED97
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<GremlinMerc>(),
					ModelDb.Monster<FatGremlin>(),
					ModelDb.Monster<SneakyGremlin>()
				});
			}
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x00250BBC File Offset: 0x0024EDBC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<GremlinMerc>().ToMutable(), "merc"));
		}

		// Token: 0x04002529 RID: 9513
		public const string mercSlot = "merc";

		// Token: 0x0400252A RID: 9514
		public const string sneakySlot = "sneaky";

		// Token: 0x0400252B RID: 9515
		public const string fatSlot = "fat";
	}
}
