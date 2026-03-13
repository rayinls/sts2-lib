using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200080C RID: 2060
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CeremonialBeastBoss : EncounterModel
	{
		// Token: 0x170018BB RID: 6331
		// (get) Token: 0x06006399 RID: 25497 RVA: 0x0024FF64 File Offset: 0x0024E164
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x170018BC RID: 6332
		// (get) Token: 0x0600639A RID: 25498 RVA: 0x0024FF67 File Offset: 0x0024E167
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act1_boss_ceremonial_beast";
			}
		}

		// Token: 0x0600639B RID: 25499 RVA: 0x0024FF6E File Offset: 0x0024E16E
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x0600639C RID: 25500 RVA: 0x0024FF75 File Offset: 0x0024E175
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x170018BD RID: 6333
		// (get) Token: 0x0600639D RID: 25501 RVA: 0x0024FF86 File Offset: 0x0024E186
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x0600639E RID: 25502 RVA: 0x0024FF89 File Offset: 0x0024E189
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<CeremonialBeast>());
			}
		}

		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x0600639F RID: 25503 RVA: 0x0024FF95 File Offset: 0x0024E195
		[Nullable(new byte[] { 2, 1 })]
		public override IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Affliction<Ringing>().OverlayPath);
			}
		}

		// Token: 0x060063A0 RID: 25504 RVA: 0x0024FFA6 File Offset: 0x0024E1A6
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<CeremonialBeast>().ToMutable(), null));
		}
	}
}
