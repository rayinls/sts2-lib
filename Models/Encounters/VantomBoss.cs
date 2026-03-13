using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200085B RID: 2139
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VantomBoss : EncounterModel
	{
		// Token: 0x170019CF RID: 6607
		// (get) Token: 0x06006577 RID: 25975 RVA: 0x002527C5 File Offset: 0x002509C5
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x170019D0 RID: 6608
		// (get) Token: 0x06006578 RID: 25976 RVA: 0x002527C8 File Offset: 0x002509C8
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x170019D1 RID: 6609
		// (get) Token: 0x06006579 RID: 25977 RVA: 0x002527E9 File Offset: 0x002509E9
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x002527EC File Offset: 0x002509EC
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x002527F3 File Offset: 0x002509F3
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x170019D2 RID: 6610
		// (get) Token: 0x0600657C RID: 25980 RVA: 0x00252804 File Offset: 0x00250A04
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019D3 RID: 6611
		// (get) Token: 0x0600657D RID: 25981 RVA: 0x00252807 File Offset: 0x00250A07
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act1_boss_vantom";
			}
		}

		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x0600657E RID: 25982 RVA: 0x0025280E File Offset: 0x00250A0E
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<Vantom>());
			}
		}

		// Token: 0x0600657F RID: 25983 RVA: 0x0025281A File Offset: 0x00250A1A
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Vantom>().ToMutable(), null));
		}
	}
}
