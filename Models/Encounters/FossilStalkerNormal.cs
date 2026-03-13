using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200081F RID: 2079
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FossilStalkerNormal : EncounterModel
	{
		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06006414 RID: 25620 RVA: 0x00250ABD File Offset: 0x0024ECBD
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001906 RID: 6406
		// (get) Token: 0x06006415 RID: 25621 RVA: 0x00250AC0 File Offset: 0x0024ECC0
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<FossilStalker>());
			}
		}

		// Token: 0x06006416 RID: 25622 RVA: 0x00250ACC File Offset: 0x0024ECCC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<FossilStalker>().ToMutable(), null));
		}
	}
}
