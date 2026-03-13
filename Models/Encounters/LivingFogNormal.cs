using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200082C RID: 2092
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LivingFogNormal : EncounterModel
	{
		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06006462 RID: 25698 RVA: 0x00250F6E File Offset: 0x0024F16E
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06006463 RID: 25699 RVA: 0x00250F71 File Offset: 0x0024F171
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06006464 RID: 25700 RVA: 0x00250F74 File Offset: 0x0024F174
		public override IReadOnlyList<string> Slots
		{
			get
			{
				return new <>z__ReadOnlyArray<string>(new string[] { "bomb1", "bomb2", "bomb3", "bomb4", "bomb5", "livingFog" });
			}
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x00250FB1 File Offset: 0x0024F1B1
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06006466 RID: 25702 RVA: 0x00250FB8 File Offset: 0x0024F1B8
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<LivingFog>(),
					ModelDb.Monster<GasBomb>()
				});
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06006467 RID: 25703 RVA: 0x00250FD5 File Offset: 0x0024F1D5
		[Nullable(new byte[] { 2, 1 })]
		public override IEnumerable<string> ExtraAssetPaths
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return new <>z__ReadOnlySingleElementList<string>(ModelDb.Affliction<Smog>().OverlayPath);
			}
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x00250FE6 File Offset: 0x0024F1E6
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlySingleElementList<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>(ModelDb.Monster<LivingFog>().ToMutable(), "livingFog"));
		}

		// Token: 0x0400252E RID: 9518
		private const string _livingFogSlot = "livingFog";

		// Token: 0x0400252F RID: 9519
		private const string _bombSlotPrefix = "bomb";
	}
}
