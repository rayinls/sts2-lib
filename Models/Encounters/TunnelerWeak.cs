using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000858 RID: 2136
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TunnelerWeak : EncounterModel
	{
		// Token: 0x170019C4 RID: 6596
		// (get) Token: 0x06006564 RID: 25956 RVA: 0x00252602 File Offset: 0x00250802
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Burrower);
			}
		}

		// Token: 0x170019C5 RID: 6597
		// (get) Token: 0x06006565 RID: 25957 RVA: 0x0025260A File Offset: 0x0025080A
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019C6 RID: 6598
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x0025260D File Offset: 0x0025080D
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019C7 RID: 6599
		// (get) Token: 0x06006567 RID: 25959 RVA: 0x00252610 File Offset: 0x00250810
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Tunneler>());
			}
		}

		// Token: 0x06006568 RID: 25960 RVA: 0x0025261C File Offset: 0x0025081C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Tunneler>().ToMutable(), null));
		}
	}
}
