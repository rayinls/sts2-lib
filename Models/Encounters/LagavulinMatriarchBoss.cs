using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200082B RID: 2091
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LagavulinMatriarchBoss : EncounterModel
	{
		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x0600645B RID: 25691 RVA: 0x00250F15 File Offset: 0x0024F115
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x00250F18 File Offset: 0x0024F118
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x00250F1F File Offset: 0x0024F11F
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x0600645E RID: 25694 RVA: 0x00250F40 File Offset: 0x0024F140
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x0600645F RID: 25695 RVA: 0x00250F43 File Offset: 0x0024F143
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<LagavulinMatriarch>());
			}
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x00250F4F File Offset: 0x0024F14F
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<LagavulinMatriarch>().ToMutable(), null));
		}
	}
}
