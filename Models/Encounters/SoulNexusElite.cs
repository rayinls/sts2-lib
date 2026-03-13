using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200084B RID: 2123
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoulNexusElite : EncounterModel
	{
		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06006517 RID: 25879 RVA: 0x002520CA File Offset: 0x002502CA
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06006518 RID: 25880 RVA: 0x002520CD File Offset: 0x002502CD
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SoulNexus>());
			}
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x002520D9 File Offset: 0x002502D9
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SoulNexus>().ToMutable(), null));
		}
	}
}
