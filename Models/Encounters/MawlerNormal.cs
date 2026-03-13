using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200082E RID: 2094
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MawlerNormal : EncounterModel
	{
		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x0600646E RID: 25710 RVA: 0x00251037 File Offset: 0x0024F237
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x0600646F RID: 25711 RVA: 0x0025103A File Offset: 0x0024F23A
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Mawler>());
			}
		}

		// Token: 0x06006470 RID: 25712 RVA: 0x00251046 File Offset: 0x0024F246
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Mawler>().ToMutable(), null));
		}
	}
}
