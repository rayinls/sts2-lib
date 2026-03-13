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
	// Token: 0x0200080F RID: 2063
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CorpseSlugsNormal : EncounterModel
	{
		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x060063AB RID: 25515 RVA: 0x002500C5 File Offset: 0x0024E2C5
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Slugs);
			}
		}

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x060063AC RID: 25516 RVA: 0x002500CE File Offset: 0x0024E2CE
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x060063AD RID: 25517 RVA: 0x002500D1 File Offset: 0x0024E2D1
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<CorpseSlug>());
			}
		}

		// Token: 0x060063AE RID: 25518 RVA: 0x002500E0 File Offset: 0x0024E2E0
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected unsafe override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			int num = 3;
			List<ValueTuple<MonsterModel, string>> list = new List<ValueTuple<MonsterModel, string>>(num);
			CollectionsMarshal.SetCount<ValueTuple<MonsterModel, string>>(list, num);
			Span<ValueTuple<MonsterModel, string>> span = CollectionsMarshal.AsSpan<ValueTuple<MonsterModel, string>>(list);
			int num2 = 0;
			*span[num2] = new ValueTuple<MonsterModel, string>(ModelDb.Monster<CorpseSlug>().ToMutable(), null);
			num2++;
			*span[num2] = new ValueTuple<MonsterModel, string>(ModelDb.Monster<CorpseSlug>().ToMutable(), null);
			num2++;
			*span[num2] = new ValueTuple<MonsterModel, string>(ModelDb.Monster<CorpseSlug>().ToMutable(), null);
			List<ValueTuple<MonsterModel, string>> list2 = list;
			CorpseSlug.EnsureCorpseSlugsStartWithDifferentMoves(list2.Select((ValueTuple<MonsterModel, string> kvp) => kvp.Item1), base.Rng);
			return list2;
		}
	}
}
