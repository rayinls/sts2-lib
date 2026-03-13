using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200084F RID: 2127
	[NullableContext(1)]
	[Nullable(0)]
	public class TheArchitectEventEncounter : EncounterModel
	{
		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x002521B7 File Offset: 0x002503B7
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x0600652D RID: 25901 RVA: 0x002521BA File Offset: 0x002503BA
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x0600652E RID: 25902 RVA: 0x002521BD File Offset: 0x002503BD
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Architect>());
			}
		}

		// Token: 0x0600652F RID: 25903 RVA: 0x002521C9 File Offset: 0x002503C9
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Architect>().ToMutable(), null));
		}
	}
}
