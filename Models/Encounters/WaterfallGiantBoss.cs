using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200085D RID: 2141
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WaterfallGiantBoss : EncounterModel
	{
		// Token: 0x170019D8 RID: 6616
		// (get) Token: 0x06006586 RID: 25990 RVA: 0x00252878 File Offset: 0x00250A78
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x06006587 RID: 25991 RVA: 0x0025287B File Offset: 0x00250A7B
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x170019D9 RID: 6617
		// (get) Token: 0x06006588 RID: 25992 RVA: 0x00252882 File Offset: 0x00250A82
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act1_b_boss_waterfall_giant";
			}
		}

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06006589 RID: 25993 RVA: 0x00252889 File Offset: 0x00250A89
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x0600658A RID: 25994 RVA: 0x002528AA File Offset: 0x00250AAA
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x0600658B RID: 25995 RVA: 0x002528AD File Offset: 0x00250AAD
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<WaterfallGiant>());
			}
		}

		// Token: 0x0600658C RID: 25996 RVA: 0x002528B9 File Offset: 0x00250AB9
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<WaterfallGiant>().ToMutable(), null));
		}
	}
}
