using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000837 RID: 2103
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PhantasmalGardenersElite : EncounterModel
	{
		// Token: 0x17001954 RID: 6484
		// (get) Token: 0x060064A1 RID: 25761 RVA: 0x002513C8 File Offset: 0x0024F5C8
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "first", "second", "third", "fourth" });
			}
		}

		// Token: 0x17001955 RID: 6485
		// (get) Token: 0x060064A2 RID: 25762 RVA: 0x002513F5 File Offset: 0x0024F5F5
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Elite;
			}
		}

		// Token: 0x17001956 RID: 6486
		// (get) Token: 0x060064A3 RID: 25763 RVA: 0x002513F8 File Offset: 0x0024F5F8
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x002513FB File Offset: 0x0024F5FB
		public override float GetCameraScaling()
		{
			return 0.85f;
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x00251402 File Offset: 0x0024F602
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 40f;
		}

		// Token: 0x17001957 RID: 6487
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x00251413 File Offset: 0x0024F613
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<PhantasmalGardener>());
			}
		}

		// Token: 0x060064A7 RID: 25767 RVA: 0x00251420 File Offset: 0x0024F620
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<PhantasmalGardener>().ToMutable(), "first"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<PhantasmalGardener>().ToMutable(), "second"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<PhantasmalGardener>().ToMutable(), "third"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<PhantasmalGardener>().ToMutable(), "fourth")
			});
		}

		// Token: 0x04002536 RID: 9526
		public const string firstSlot = "first";

		// Token: 0x04002537 RID: 9527
		public const string secondSlot = "second";

		// Token: 0x04002538 RID: 9528
		public const string thirdSlot = "third";

		// Token: 0x04002539 RID: 9529
		public const string fourthSlot = "fourth";
	}
}
