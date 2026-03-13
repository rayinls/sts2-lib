using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000835 RID: 2101
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OvicopterNormal : EncounterModel
	{
		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06006495 RID: 25749 RVA: 0x002512D8 File Offset: 0x0024F4D8
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700194F RID: 6479
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x002512DB File Offset: 0x0024F4DB
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "egg1", "egg2", "egg3", "egg4", "egg5", "ovicopter" });
			}
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x00251318 File Offset: 0x0024F518
		public override float GetCameraScaling()
		{
			return 0.8f;
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x0025131F File Offset: 0x0024F51F
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f + Vector2.Left * 100f;
		}

		// Token: 0x17001950 RID: 6480
		// (get) Token: 0x06006499 RID: 25753 RVA: 0x00251344 File Offset: 0x0024F544
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001951 RID: 6481
		// (get) Token: 0x0600649A RID: 25754 RVA: 0x00251347 File Offset: 0x0024F547
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<Ovicopter>(),
					ModelDb.Monster<ToughEgg>()
				});
			}
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x00251364 File Offset: 0x0024F564
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new List<ValueTuple<MonsterModel, string>>
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Ovicopter>().ToMutable(), "ovicopter")
			};
		}

		// Token: 0x04002534 RID: 9524
		private const string _ovicopterSlot = "ovicopter";

		// Token: 0x04002535 RID: 9525
		private const string _eggSlotPrefix = "egg";
	}
}
