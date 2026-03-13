using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000828 RID: 2088
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class KaiserCrabBoss : EncounterModel
	{
		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x0600643C RID: 25660 RVA: 0x00250CF9 File Offset: 0x0024EEF9
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x00250D1A File Offset: 0x0024EF1A
		public override float GetCameraScaling()
		{
			return 0.75f;
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x00250D21 File Offset: 0x0024EF21
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 35f;
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x0600643F RID: 25663 RVA: 0x00250D32 File Offset: 0x0024EF32
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x00250D35 File Offset: 0x0024EF35
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700191E RID: 6430
		// (get) Token: 0x06006441 RID: 25665 RVA: 0x00250D38 File Offset: 0x0024EF38
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x06006442 RID: 25666 RVA: 0x00250D3B File Offset: 0x0024EF3B
		public override bool FullyCenterPlayers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001920 RID: 6432
		// (get) Token: 0x06006443 RID: 25667 RVA: 0x00250D3E File Offset: 0x0024EF3E
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "crusher", "rocket" });
			}
		}

		// Token: 0x17001921 RID: 6433
		// (get) Token: 0x06006444 RID: 25668 RVA: 0x00250D5B File Offset: 0x0024EF5B
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06006445 RID: 25669 RVA: 0x00250D5E File Offset: 0x0024EF5E
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<Crusher>(),
					ModelDb.Monster<Rocket>()
				});
			}
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x00250D7C File Offset: 0x0024EF7C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Crusher>().ToMutable(), "crusher"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Rocket>().ToMutable(), "rocket")
			});
		}

		// Token: 0x0400252C RID: 9516
		private const string _crusherSlot = "crusher";

		// Token: 0x0400252D RID: 9517
		private const string _rocketSlot = "rocket";
	}
}
