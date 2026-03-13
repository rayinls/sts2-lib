using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200082A RID: 2090
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KnowledgeDemonBoss : EncounterModel
	{
		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x00250EA1 File Offset: 0x0024F0A1
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x00250EA4 File Offset: 0x0024F0A4
		public override float GetCameraScaling()
		{
			return 0.85f;
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x00250EAB File Offset: 0x0024F0AB
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 70f;
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x00250EBC File Offset: 0x0024F0BC
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act2_boss_knowledge_demon";
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x00250EC3 File Offset: 0x0024F0C3
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06006456 RID: 25686 RVA: 0x00250EE4 File Offset: 0x0024F0E4
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06006457 RID: 25687 RVA: 0x00250EE7 File Offset: 0x0024F0E7
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<KnowledgeDemon>());
			}
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x00250EF3 File Offset: 0x0024F0F3
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<KnowledgeDemon>().ToMutable(), null));
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x06006459 RID: 25689 RVA: 0x00250F0A File Offset: 0x0024F10A
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}
	}
}
