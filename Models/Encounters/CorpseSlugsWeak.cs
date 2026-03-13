using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000810 RID: 2064
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CorpseSlugsWeak : EncounterModel
	{
		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x060063B0 RID: 25520 RVA: 0x0025019E File Offset: 0x0024E39E
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Slugs);
			}
		}

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x060063B1 RID: 25521 RVA: 0x002501A7 File Offset: 0x0024E3A7
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x060063B2 RID: 25522 RVA: 0x002501AA File Offset: 0x0024E3AA
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x060063B3 RID: 25523 RVA: 0x002501AD File Offset: 0x0024E3AD
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<CorpseSlug>());
			}
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x002501BC File Offset: 0x0024E3BC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected unsafe override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			int num = 2;
			List<ValueTuple<MonsterModel, string>> list = new List<ValueTuple<MonsterModel, string>>(num);
			CollectionsMarshal.SetCount<ValueTuple<MonsterModel, string>>(list, num);
			Span<ValueTuple<MonsterModel, string>> span = CollectionsMarshal.AsSpan<ValueTuple<MonsterModel, string>>(list);
			int num2 = 0;
			*span[num2] = new ValueTuple<MonsterModel, string>(ModelDb.Monster<CorpseSlug>().ToMutable(), null);
			num2++;
			*span[num2] = new ValueTuple<MonsterModel, string>(ModelDb.Monster<CorpseSlug>().ToMutable(), null);
			List<ValueTuple<MonsterModel, string>> list2 = list;
			CorpseSlug.EnsureCorpseSlugsStartWithDifferentMoves(list2.Select((ValueTuple<MonsterModel, string> kvp) => kvp.Item1), base.Rng);
			return list2;
		}
	}
}
