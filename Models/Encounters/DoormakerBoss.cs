using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000817 RID: 2071
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DoormakerBoss : EncounterModel
	{
		// Token: 0x170018DE RID: 6366
		// (get) Token: 0x060063D7 RID: 25559 RVA: 0x00250515 File Offset: 0x0024E715
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x170018DF RID: 6367
		// (get) Token: 0x060063D8 RID: 25560 RVA: 0x00250518 File Offset: 0x0024E718
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "doormaker", "door" });
			}
		}

		// Token: 0x170018E0 RID: 6368
		// (get) Token: 0x060063D9 RID: 25561 RVA: 0x00250535 File Offset: 0x0024E735
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170018E1 RID: 6369
		// (get) Token: 0x060063DA RID: 25562 RVA: 0x00250538 File Offset: 0x0024E738
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x170018E2 RID: 6370
		// (get) Token: 0x060063DB RID: 25563 RVA: 0x00250559 File Offset: 0x0024E759
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act3_boss_queen";
			}
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x00250560 File Offset: 0x0024E760
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x00250567 File Offset: 0x0024E767
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 60f;
		}

		// Token: 0x170018E3 RID: 6371
		// (get) Token: 0x060063DE RID: 25566 RVA: 0x00250578 File Offset: 0x0024E778
		protected override bool HasCustomBackground
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170018E4 RID: 6372
		// (get) Token: 0x060063DF RID: 25567 RVA: 0x0025057B File Offset: 0x0024E77B
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018E5 RID: 6373
		// (get) Token: 0x060063E0 RID: 25568 RVA: 0x0025057E File Offset: 0x0024E77E
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<Door>(),
					ModelDb.Monster<Doormaker>()
				});
			}
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x0025059B File Offset: 0x0024E79B
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<Door>().ToMutable(), "door"));
		}

		// Token: 0x0400251D RID: 9501
		private const string _doorSlot = "door";

		// Token: 0x0400251E RID: 9502
		public const string doormakerSlot = "doormaker";
	}
}
