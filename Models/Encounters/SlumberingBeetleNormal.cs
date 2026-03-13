using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000848 RID: 2120
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlumberingBeetleNormal : EncounterModel
	{
		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06006504 RID: 25860 RVA: 0x00251F4D File Offset: 0x0025014D
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Workers);
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06006505 RID: 25861 RVA: 0x00251F55 File Offset: 0x00250155
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06006506 RID: 25862 RVA: 0x00251F58 File Offset: 0x00250158
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<BowlbugRock>(),
					ModelDb.Monster<SlumberingBeetle>(),
					ModelDb.Monster<BowlbugSilk>()
				});
			}
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00251F80 File Offset: 0x00250180
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<BowlbugRock>().ToMutable(), "first"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<BowlbugSilk>().ToMutable(), "second"),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<SlumberingBeetle>().ToMutable(), "third")
			});
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x00251FE9 File Offset: 0x002501E9
		public override float GetCameraScaling()
		{
			return 0.85f;
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x00251FF0 File Offset: 0x002501F0
		public override Vector2 GetCameraOffset()
		{
			return new Vector2(0f, 50f);
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x0600650A RID: 25866 RVA: 0x00252001 File Offset: 0x00250201
		public override bool HasScene
		{
			get
			{
				return true;
			}
		}
	}
}
