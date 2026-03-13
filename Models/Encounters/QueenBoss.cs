using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200083B RID: 2107
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class QueenBoss : EncounterModel
	{
		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x060064B9 RID: 25785 RVA: 0x00251637 File Offset: 0x0024F837
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x060064BA RID: 25786 RVA: 0x0025163A File Offset: 0x0024F83A
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "amalgam", "queen" });
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x060064BB RID: 25787 RVA: 0x00251657 File Offset: 0x0024F857
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act3_boss_queen";
			}
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x0025165E File Offset: 0x0024F85E
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x060064BD RID: 25789 RVA: 0x00251665 File Offset: 0x0024F865
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 60f;
		}

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x060064BE RID: 25790 RVA: 0x00251676 File Offset: 0x0024F876
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x060064BF RID: 25791 RVA: 0x00251679 File Offset: 0x0024F879
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x060064C0 RID: 25792 RVA: 0x0025167C File Offset: 0x0024F87C
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<Queen>(),
					ModelDb.Monster<TorchHeadAmalgam>()
				});
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x060064C1 RID: 25793 RVA: 0x00251699 File Offset: 0x0024F899
		[Nullable(new byte[] { 2, 1 })]
		public override IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Affliction<Bound>().OverlayPath);
			}
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x002516AC File Offset: 0x0024F8AC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<TorchHeadAmalgam>().ToMutable(), "amalgam"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Queen>().ToMutable(), "queen")
			});
		}

		// Token: 0x0400253C RID: 9532
		private const string _queenSlot = "queen";

		// Token: 0x0400253D RID: 9533
		private const string _amalgamSlot = "amalgam";
	}
}
