using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters.Mocks;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x0200085E RID: 2142
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockArtifactEncounter : EncounterModel
	{
		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x002528D8 File Offset: 0x00250AD8
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019DE RID: 6622
		// (get) Token: 0x0600658F RID: 25999 RVA: 0x002528DB File Offset: 0x00250ADB
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019DF RID: 6623
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x002528DE File Offset: 0x00250ADE
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<MockArtifactMonster>());
			}
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x002528EA File Offset: 0x00250AEA
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<MockArtifactMonster>().ToMutable(), null));
		}
	}
}
