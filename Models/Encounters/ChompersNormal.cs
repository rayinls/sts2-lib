using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200080D RID: 2061
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChompersNormal : EncounterModel
	{
		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x060063A2 RID: 25506 RVA: 0x0024FFC5 File Offset: 0x0024E1C5
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Chomper);
			}
		}

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x060063A3 RID: 25507 RVA: 0x0024FFCD File Offset: 0x0024E1CD
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x060063A4 RID: 25508 RVA: 0x0024FFD0 File Offset: 0x0024E1D0
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Chomper>());
			}
		}

		// Token: 0x060063A5 RID: 25509 RVA: 0x0024FFDC File Offset: 0x0024E1DC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			Chomper chomper = (Chomper)ModelDb.Monster<Chomper>().ToMutable();
			Chomper chomper2 = (Chomper)ModelDb.Monster<Chomper>().ToMutable();
			chomper2.ScreamFirst = true;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(chomper, null),
				new ValueTuple<MonsterModel, string>(chomper2, null)
			});
		}
	}
}
