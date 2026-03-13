using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000844 RID: 2116
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlimesNormal : EncounterModel
	{
		// Token: 0x060064EC RID: 25836 RVA: 0x00251AA9 File Offset: 0x0024FCA9
		public override float GetCameraScaling()
		{
			return 0.9f;
		}

		// Token: 0x060064ED RID: 25837 RVA: 0x00251AB0 File Offset: 0x0024FCB0
		public override Vector2 GetCameraOffset()
		{
			return Vector2.Down * 50f;
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x060064EE RID: 25838 RVA: 0x00251AC1 File Offset: 0x0024FCC1
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x060064EF RID: 25839 RVA: 0x00251AC4 File Offset: 0x0024FCC4
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Slimes);
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x060064F0 RID: 25840 RVA: 0x00251ACC File Offset: 0x0024FCCC
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return new <>z__ReadOnlyArray<MonsterModel>(new MonsterModel[]
				{
					ModelDb.Monster<LeafSlimeS>(),
					ModelDb.Monster<TwigSlimeS>(),
					ModelDb.Monster<TwigSlimeM>(),
					ModelDb.Monster<LeafSlimeM>()
				});
			}
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x00251AFC File Offset: 0x0024FCFC
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			bool flag = base.Rng.NextBool();
			MonsterModel monsterModel = (flag ? ModelDb.Monster<LeafSlimeS>() : ModelDb.Monster<TwigSlimeS>());
			MonsterModel monsterModel2 = (flag ? ModelDb.Monster<TwigSlimeS>() : ModelDb.Monster<LeafSlimeS>());
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<TwigSlimeM>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<LeafSlimeM>().ToMutable(), null),
				new ValueTuple<MonsterModel, string>(monsterModel.ToMutable(), null),
				new ValueTuple<MonsterModel, string>(monsterModel2.ToMutable(), null)
			});
		}
	}
}
