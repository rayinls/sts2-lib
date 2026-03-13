using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000824 RID: 2084
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HauntedShipNormal : EncounterModel
	{
		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x0600642C RID: 25644 RVA: 0x00250BDF File Offset: 0x0024EDDF
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x00250BE2 File Offset: 0x0024EDE2
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<HauntedShip>());
			}
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x00250BEE File Offset: 0x0024EDEE
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<HauntedShip>().ToMutable(), null));
		}
	}
}
