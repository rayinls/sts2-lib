using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000847 RID: 2119
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SludgeSpinnerWeak : EncounterModel
	{
		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x00251F1C File Offset: 0x0025011C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x00251F1F File Offset: 0x0025011F
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x00251F22 File Offset: 0x00250122
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SludgeSpinner>());
			}
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x00251F2E File Offset: 0x0025012E
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SludgeSpinner>().ToMutable(), null));
		}
	}
}
