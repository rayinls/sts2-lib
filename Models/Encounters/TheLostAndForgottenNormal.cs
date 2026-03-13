using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000852 RID: 2130
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheLostAndForgottenNormal : EncounterModel
	{
		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06006545 RID: 25925 RVA: 0x0025233B File Offset: 0x0025053B
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06006546 RID: 25926 RVA: 0x0025233E File Offset: 0x0025053E
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<TheLost>(),
					ModelDb.Monster<TheForgotten>()
				});
			}
		}

		// Token: 0x06006547 RID: 25927 RVA: 0x0025235B File Offset: 0x0025055B
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<TheLost>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<TheForgotten>().ToMutable(), null)
			});
		}
	}
}
