using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000836 RID: 2102
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OwlMagistrateNormal : EncounterModel
	{
		// Token: 0x17001952 RID: 6482
		// (get) Token: 0x0600649D RID: 25757 RVA: 0x0025139A File Offset: 0x0024F59A
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001953 RID: 6483
		// (get) Token: 0x0600649E RID: 25758 RVA: 0x0025139D File Offset: 0x0024F59D
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<OwlMagistrate>());
			}
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x002513A9 File Offset: 0x0024F5A9
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<OwlMagistrate>().ToMutable(), null));
		}
	}
}
