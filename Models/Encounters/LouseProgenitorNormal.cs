using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200082D RID: 2093
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LouseProgenitorNormal : EncounterModel
	{
		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x0600646A RID: 25706 RVA: 0x00251009 File Offset: 0x0024F209
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x0600646B RID: 25707 RVA: 0x0025100C File Offset: 0x0024F20C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<LouseProgenitor>());
			}
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x00251018 File Offset: 0x0024F218
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<LouseProgenitor>().ToMutable(), null));
		}
	}
}
