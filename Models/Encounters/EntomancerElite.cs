using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000818 RID: 2072
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EntomancerElite : EncounterModel
	{
		// Token: 0x170018E6 RID: 6374
		// (get) Token: 0x060063E3 RID: 25571 RVA: 0x002505BE File Offset: 0x0024E7BE
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x060063E4 RID: 25572 RVA: 0x002505C1 File Offset: 0x0024E7C1
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Entomancer>());
			}
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x002505CD File Offset: 0x0024E7CD
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Entomancer>().ToMutable(), null));
		}
	}
}
