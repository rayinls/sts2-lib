using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000851 RID: 2129
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheKinBoss : EncounterModel
	{
		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06006539 RID: 25913 RVA: 0x0025222E File Offset: 0x0025042E
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x0600653A RID: 25914 RVA: 0x00252231 File Offset: 0x00250431
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x0600653B RID: 25915 RVA: 0x00252234 File Offset: 0x00250434
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600653C RID: 25916 RVA: 0x00252237 File Offset: 0x00250437
		public override float GetCameraScaling()
		{
			return 0.85f;
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x0025223E File Offset: 0x0025043E
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x170019AD RID: 6573
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x0025224F File Offset: 0x0025044F
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act1_boss_the_kin";
			}
		}

		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x0600653F RID: 25919 RVA: 0x00252256 File Offset: 0x00250456
		public override string BossNodePath
		{
			get
			{
				return "res://images/map/placeholder/" + base.Id.Entry.ToLowerInvariant() + "_icon";
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06006540 RID: 25920 RVA: 0x00252277 File Offset: 0x00250477
		[Nullable(2)]
		public override MegaSkeletonDataResource BossNodeSpineResource
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06006541 RID: 25921 RVA: 0x0025227A File Offset: 0x0025047A
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "slot1", "slot2", "leaderSlot" });
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06006542 RID: 25922 RVA: 0x0025229F File Offset: 0x0025049F
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<KinFollower>(),
					ModelDb.Monster<KinPriest>()
				});
			}
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x002522BC File Offset: 0x002504BC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			KinFollower kinFollower = (KinFollower)ModelDb.Monster<KinFollower>().ToMutable();
			kinFollower.StartsWithDance = true;
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(kinFollower, "slot1"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<KinFollower>().ToMutable(), "slot2"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<KinPriest>().ToMutable(), "leaderSlot")
			});
		}
	}
}
