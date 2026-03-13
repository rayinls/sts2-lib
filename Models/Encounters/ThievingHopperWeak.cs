using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000854 RID: 2132
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThievingHopperWeak : EncounterModel
	{
		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x0600654F RID: 25935 RVA: 0x002523F0 File Offset: 0x002505F0
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06006550 RID: 25936 RVA: 0x002523F3 File Offset: 0x002505F3
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Thieves);
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06006551 RID: 25937 RVA: 0x002523FB File Offset: 0x002505FB
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06006552 RID: 25938 RVA: 0x002523FE File Offset: 0x002505FE
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<ThievingHopper>());
			}
		}

		// Token: 0x06006553 RID: 25939 RVA: 0x0025240A File Offset: 0x0025060A
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<ThievingHopper>().ToMutable(), null));
		}
	}
}
