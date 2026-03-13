using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000830 RID: 2096
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MysteriousKnightEventEncounter : EncounterModel
	{
		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06006478 RID: 25720 RVA: 0x002510AB File Offset: 0x0024F2AB
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06006479 RID: 25721 RVA: 0x002510AE File Offset: 0x0024F2AE
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<MysteriousKnight>());
			}
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x002510BA File Offset: 0x0024F2BA
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<MysteriousKnight>().ToMutable(), null));
		}
	}
}
