using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000816 RID: 2070
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DevotedSculptorWeak : EncounterModel
	{
		// Token: 0x170018DB RID: 6363
		// (get) Token: 0x060063D2 RID: 25554 RVA: 0x002504E4 File Offset: 0x0024E6E4
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018DC RID: 6364
		// (get) Token: 0x060063D3 RID: 25555 RVA: 0x002504E7 File Offset: 0x0024E6E7
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018DD RID: 6365
		// (get) Token: 0x060063D4 RID: 25556 RVA: 0x002504EA File Offset: 0x0024E6EA
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<DevotedSculptor>());
			}
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x002504F6 File Offset: 0x0024E6F6
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<DevotedSculptor>().ToMutable(), null));
		}
	}
}
