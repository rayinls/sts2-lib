using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000841 RID: 2113
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShrinkerBeetleWeak : EncounterModel
	{
		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x060064DE RID: 25822 RVA: 0x00251A14 File Offset: 0x0024FC14
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x060064DF RID: 25823 RVA: 0x00251A17 File Offset: 0x0024FC17
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Shrinker);
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x060064E0 RID: 25824 RVA: 0x00251A1F File Offset: 0x0024FC1F
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x060064E1 RID: 25825 RVA: 0x00251A22 File Offset: 0x0024FC22
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<ShrinkerBeetle>());
			}
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x00251A2E File Offset: 0x0024FC2E
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<ShrinkerBeetle>().ToMutable(), null));
		}
	}
}
