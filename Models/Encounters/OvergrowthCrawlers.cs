using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000834 RID: 2100
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OvergrowthCrawlers : EncounterModel
	{
		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x06006490 RID: 25744 RVA: 0x00251260 File Offset: 0x0024F460
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x00251263 File Offset: 0x0024F463
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterTag>(new EncounterTag[]
				{
					EncounterTag.Shrinker,
					EncounterTag.Crawler
				});
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06006492 RID: 25746 RVA: 0x00251278 File Offset: 0x0024F478
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<ShrinkerBeetle>(),
					ModelDb.Monster<FuzzyWurmCrawler>()
				});
			}
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x00251295 File Offset: 0x0024F495
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<ShrinkerBeetle>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<FuzzyWurmCrawler>().ToMutable(), null)
			});
		}
	}
}
