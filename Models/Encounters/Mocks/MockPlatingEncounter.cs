using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters.Mocks;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters.Mocks
{
	// Token: 0x02000863 RID: 2147
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockPlatingEncounter : EncounterModel
	{
		// Token: 0x170019ED RID: 6637
		// (get) Token: 0x060065A8 RID: 26024 RVA: 0x002529D0 File Offset: 0x00250BD0
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170019EE RID: 6638
		// (get) Token: 0x060065A9 RID: 26025 RVA: 0x002529D3 File Offset: 0x00250BD3
		public override bool IsDebugEncounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019EF RID: 6639
		// (get) Token: 0x060065AA RID: 26026 RVA: 0x002529D6 File Offset: 0x00250BD6
		// (set) Token: 0x060065AB RID: 26027 RVA: 0x002529DE File Offset: 0x00250BDE
		public int PlatingAmount
		{
			get
			{
				return this._platingAmount;
			}
			set
			{
				base.AssertMutable();
				this._platingAmount = value;
			}
		}

		// Token: 0x170019F0 RID: 6640
		// (get) Token: 0x060065AC RID: 26028 RVA: 0x002529ED File Offset: 0x00250BED
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<MockPlatingMonster>());
			}
		}

		// Token: 0x060065AD RID: 26029 RVA: 0x002529FC File Offset: 0x00250BFC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			MonsterModel monsterModel = ModelDb.Monster<MockPlatingMonster>().ToMutable();
			((MockPlatingMonster)monsterModel).PlatingAmount = this.PlatingAmount;
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(monsterModel, null));
		}

		// Token: 0x04002546 RID: 9542
		private int _platingAmount = 1;
	}
}
