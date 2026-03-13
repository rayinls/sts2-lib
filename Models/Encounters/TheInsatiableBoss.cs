using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000850 RID: 2128
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheInsatiableBoss : EncounterModel
	{
		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06006531 RID: 25905 RVA: 0x002521E8 File Offset: 0x002503E8
		public override string CustomBgm
		{
			get
			{
				return "event:/music/act2_boss_the_insatiable";
			}
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x002521EF File Offset: 0x002503EF
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Boss;
			}
		}

		// Token: 0x06006533 RID: 25907 RVA: 0x002521F2 File Offset: 0x002503F2
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06006534 RID: 25908 RVA: 0x002521F9 File Offset: 0x002503F9
		protected override bool HasCustomBackground
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06006535 RID: 25909 RVA: 0x002521FC File Offset: 0x002503FC
		public override string AmbientSfx
		{
			get
			{
				return "event:/sfx/ambience/act2_ambience_the_insatiable";
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06006536 RID: 25910 RVA: 0x00252203 File Offset: 0x00250403
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<MonsterModel>(ModelDb.Monster<TheInsatiable>());
			}
		}

		// Token: 0x06006537 RID: 25911 RVA: 0x0025220F File Offset: 0x0025040F
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<TheInsatiable>().ToMutable(), null));
		}
	}
}
