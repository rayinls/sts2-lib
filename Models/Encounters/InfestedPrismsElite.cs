using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000826 RID: 2086
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfestedPrismsElite : EncounterModel
	{
		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x06006434 RID: 25652 RVA: 0x00250C3B File Offset: 0x0024EE3B
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x00250C3E File Offset: 0x0024EE3E
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<InfestedPrism>().ToMutable(), null));
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x00250C55 File Offset: 0x0024EE55
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<InfestedPrism>());
			}
		}
	}
}
