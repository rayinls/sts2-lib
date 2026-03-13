using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000815 RID: 2069
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeprecatedEncounter : EncounterModel
	{
		// Token: 0x170018D8 RID: 6360
		// (get) Token: 0x060063CD RID: 25549 RVA: 0x002504C8 File Offset: 0x0024E6C8
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018D9 RID: 6361
		// (get) Token: 0x060063CE RID: 25550 RVA: 0x002504CB File Offset: 0x0024E6CB
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018DA RID: 6362
		// (get) Token: 0x060063CF RID: 25551 RVA: 0x002504CE File Offset: 0x0024E6CE
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return Array.Empty<MonsterModel>();
			}
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x002504D5 File Offset: 0x0024E6D5
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return Array.Empty<ValueTuple<MonsterModel, string>>();
		}
	}
}
