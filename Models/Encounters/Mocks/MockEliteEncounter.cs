using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x02000860 RID: 2144
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockEliteEncounter : EncounterModel
	{
		// Token: 0x170019E3 RID: 6627
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x0025293A File Offset: 0x00250B3A
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x170019E4 RID: 6628
		// (get) Token: 0x06006599 RID: 26009 RVA: 0x0025293D File Offset: 0x00250B3D
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019E5 RID: 6629
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x00252940 File Offset: 0x00250B40
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BigDummy>());
			}
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0025294C File Offset: 0x00250B4C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<BigDummy>().ToMutable(), null));
		}
	}
}
