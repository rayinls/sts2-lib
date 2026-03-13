using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000812 RID: 2066
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CultistsNormal : EncounterModel
	{
		// Token: 0x170018CE RID: 6350
		// (get) Token: 0x060063BA RID: 25530 RVA: 0x00250287 File Offset: 0x0024E487
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018CF RID: 6351
		// (get) Token: 0x060063BB RID: 25531 RVA: 0x0025028A File Offset: 0x0024E48A
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return CultistsNormal._cultists;
			}
		}

		// Token: 0x060063BC RID: 25532 RVA: 0x00250291 File Offset: 0x0024E491
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<CalcifiedCultist>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<DampCultist>().ToMutable(), null)
			});
		}

		// Token: 0x0400251A RID: 9498
		private static readonly MonsterModel[] _cultists = new MonsterModel[]
		{
			ModelDb.Monster<CalcifiedCultist>(),
			ModelDb.Monster<DampCultist>()
		};
	}
}
