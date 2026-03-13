using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000821 RID: 2081
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FuzzyWurmCrawlerWeak : EncounterModel
	{
		// Token: 0x17001909 RID: 6409
		// (get) Token: 0x0600641C RID: 25628 RVA: 0x00250B19 File Offset: 0x0024ED19
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Crawler);
			}
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x0600641D RID: 25629 RVA: 0x00250B21 File Offset: 0x0024ED21
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x0600641E RID: 25630 RVA: 0x00250B24 File Offset: 0x0024ED24
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x0600641F RID: 25631 RVA: 0x00250B27 File Offset: 0x0024ED27
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<FuzzyWurmCrawler>());
			}
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x00250B33 File Offset: 0x0024ED33
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<FuzzyWurmCrawler>().ToMutable(), null));
		}
	}
}
