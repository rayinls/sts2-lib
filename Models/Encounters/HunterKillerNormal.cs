using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000825 RID: 2085
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HunterKillerNormal : EncounterModel
	{
		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06006430 RID: 25648 RVA: 0x00250C0D File Offset: 0x0024EE0D
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x06006431 RID: 25649 RVA: 0x00250C10 File Offset: 0x0024EE10
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<HunterKiller>());
			}
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x00250C1C File Offset: 0x0024EE1C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<HunterKiller>().ToMutable(), null));
		}
	}
}
