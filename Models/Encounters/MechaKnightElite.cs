using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200082F RID: 2095
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MechaKnightElite : EncounterModel
	{
		// Token: 0x06006472 RID: 25714 RVA: 0x00251065 File Offset: 0x0024F265
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x06006473 RID: 25715 RVA: 0x0025106C File Offset: 0x0024F26C
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06006474 RID: 25716 RVA: 0x0025107D File Offset: 0x0024F27D
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06006475 RID: 25717 RVA: 0x00251080 File Offset: 0x0024F280
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<MechaKnight>());
			}
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x0025108C File Offset: 0x0024F28C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<MechaKnight>().ToMutable(), null));
		}
	}
}
