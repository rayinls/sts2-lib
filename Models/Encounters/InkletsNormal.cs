using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000827 RID: 2087
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InkletsNormal : EncounterModel
	{
		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x06006438 RID: 25656 RVA: 0x00250C69 File Offset: 0x0024EE69
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06006439 RID: 25657 RVA: 0x00250C6C File Offset: 0x0024EE6C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Inklet>());
			}
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x00250C78 File Offset: 0x0024EE78
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			Inklet inklet = (Inklet)ModelDb.Monster<Inklet>().ToMutable();
			Inklet inklet2 = (Inklet)ModelDb.Monster<Inklet>().ToMutable();
			Inklet inklet3 = (Inklet)ModelDb.Monster<Inklet>().ToMutable();
			inklet2.MiddleInklet = true;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(inklet, null),
				new ValueTuple<MonsterModel, string>(inklet2, null),
				new ValueTuple<MonsterModel, string>(inklet3, null)
			});
		}
	}
}
