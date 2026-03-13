using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000849 RID: 2121
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SnappingJaxfruitNormal : EncounterModel
	{
		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x0600650C RID: 25868 RVA: 0x0025200C File Offset: 0x0025020C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x0600650D RID: 25869 RVA: 0x0025200F File Offset: 0x0025020F
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Mushroom);
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x0600650E RID: 25870 RVA: 0x00252018 File Offset: 0x00250218
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<SnappingJaxfruit>(),
					ModelDb.Monster<Flyconid>()
				});
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x00252035 File Offset: 0x00250235
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<SnappingJaxfruit>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Flyconid>().ToMutable(), null)
			});
		}
	}
}
