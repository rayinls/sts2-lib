using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000831 RID: 2097
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MytesNormal : EncounterModel
	{
		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x0600647C RID: 25724 RVA: 0x002510D9 File Offset: 0x0024F2D9
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "first", "second" });
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x0600647D RID: 25725 RVA: 0x002510F6 File Offset: 0x0024F2F6
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x0600647E RID: 25726 RVA: 0x002510F9 File Offset: 0x0024F2F9
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x002510FC File Offset: 0x0024F2FC
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00251103 File Offset: 0x0024F303
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06006481 RID: 25729 RVA: 0x00251114 File Offset: 0x0024F314
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Myte>());
			}
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x00251120 File Offset: 0x0024F320
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Myte>().ToMutable(), "first"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Myte>().ToMutable(), "second")
			});
		}

		// Token: 0x04002530 RID: 9520
		public const string firstSlot = "first";

		// Token: 0x04002531 RID: 9521
		public const string secondSlot = "second";
	}
}
