using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200084E RID: 2126
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TestSubjectBoss : EncounterModel
	{
		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06006524 RID: 25892 RVA: 0x0025215B File Offset: 0x0025035B
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x0025215E File Offset: 0x0025035E
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x0025217F File Offset: 0x0025037F
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act3_boss_test_subject";
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06006527 RID: 25895 RVA: 0x00252186 File Offset: 0x00250386
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06006528 RID: 25896 RVA: 0x00252189 File Offset: 0x00250389
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06006529 RID: 25897 RVA: 0x0025218C File Offset: 0x0025038C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<TestSubject>());
			}
		}

		// Token: 0x0600652A RID: 25898 RVA: 0x00252198 File Offset: 0x00250398
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<TestSubject>().ToMutable(), null));
		}
	}
}
