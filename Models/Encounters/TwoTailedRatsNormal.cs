using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200085A RID: 2138
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TwoTailedRatsNormal : EncounterModel
	{
		// Token: 0x170019CB RID: 6603
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x002526A1 File Offset: 0x002508A1
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019CC RID: 6604
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x002526A4 File Offset: 0x002508A4
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "first", "second", "third", "fourth", "fifth" });
			}
		}

		// Token: 0x170019CD RID: 6605
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x002526D9 File Offset: 0x002508D9
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x002526DC File Offset: 0x002508DC
		public override float GetCameraScaling()
		{
			return 0.85f;
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x002526E3 File Offset: 0x002508E3
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 25f;
		}

		// Token: 0x170019CE RID: 6606
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x002526F4 File Offset: 0x002508F4
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<TwoTailedRat>());
			}
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x00252700 File Offset: 0x00250900
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			TwoTailedRat twoTailedRat = (TwoTailedRat)ModelDb.Monster<TwoTailedRat>().ToMutable();
			TwoTailedRat twoTailedRat2 = (TwoTailedRat)ModelDb.Monster<TwoTailedRat>().ToMutable();
			TwoTailedRat twoTailedRat3 = (TwoTailedRat)ModelDb.Monster<TwoTailedRat>().ToMutable();
			int num = base.Rng.NextInt(3);
			twoTailedRat.StarterMoveIndex = num;
			twoTailedRat2.StarterMoveIndex = (num + 1) % 3;
			twoTailedRat3.StarterMoveIndex = (num + 2) % 3;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(twoTailedRat, this.Slots[2]),
				new ValueTuple<MonsterModel, string>(twoTailedRat2, this.Slots[3]),
				new ValueTuple<MonsterModel, string>(twoTailedRat3, this.Slots[4])
			});
		}
	}
}
