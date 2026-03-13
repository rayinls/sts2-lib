using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200080A RID: 2058
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BygoneEffigyElite : EncounterModel
	{
		// Token: 0x170018B7 RID: 6327
		// (get) Token: 0x0600638F RID: 25487 RVA: 0x0024FEF0 File Offset: 0x0024E0F0
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x06006390 RID: 25488 RVA: 0x0024FEF3 File Offset: 0x0024E0F3
		public override float GetCameraScaling()
		{
			return 0.88f;
		}

		// Token: 0x06006391 RID: 25489 RVA: 0x0024FEFA File Offset: 0x0024E0FA
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x170018B8 RID: 6328
		// (get) Token: 0x06006392 RID: 25490 RVA: 0x0024FF0B File Offset: 0x0024E10B
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<BygoneEffigy>());
			}
		}

		// Token: 0x06006393 RID: 25491 RVA: 0x0024FF17 File Offset: 0x0024E117
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<BygoneEffigy>().ToMutable(), null));
		}
	}
}
