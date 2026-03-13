using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200084C RID: 2124
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpinyToadNormal : EncounterModel
	{
		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x0600651B RID: 25883 RVA: 0x002520F8 File Offset: 0x002502F8
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x0600651C RID: 25884 RVA: 0x002520FB File Offset: 0x002502FB
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<SpinyToad>());
			}
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x00252107 File Offset: 0x00250307
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<SpinyToad>().ToMutable(), null));
		}
	}
}
