using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200081D RID: 2077
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlyconidNormal : EncounterModel
	{
		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06006408 RID: 25608 RVA: 0x00250964 File Offset: 0x0024EB64
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06006409 RID: 25609 RVA: 0x00250967 File Offset: 0x0024EB67
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlyArray<EncounterTag>(new EncounterTag[]
				{
					EncounterTag.Mushroom,
					EncounterTag.Slimes
				});
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x0600640A RID: 25610 RVA: 0x00250980 File Offset: 0x0024EB80
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				MonsterModel monsterModel = ModelDb.Monster<Flyconid>();
				MonsterModel[] mediumSlimes = FlyconidNormal._mediumSlimes;
				int num = 0;
				MonsterModel[] array = new MonsterModel[1 + mediumSlimes.Length];
				array[num] = monsterModel;
				num++;
				ReadOnlySpan<MonsterModel> readOnlySpan = new ReadOnlySpan<MonsterModel>(mediumSlimes);
				readOnlySpan.CopyTo(new Span<MonsterModel>(array).Slice(num, readOnlySpan.Length));
				num += readOnlySpan.Length;
				return new <>z__ReadOnlyArray<MonsterModel>(array);
			}
		}

		// Token: 0x0600640B RID: 25611 RVA: 0x002509E4 File Offset: 0x0024EBE4
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			return new <>z__ReadOnlyArray<ValueTuple<MonsterModel, string>>(new ValueTuple<MonsterModel, string>[]
			{
				new ValueTuple<MonsterModel, string>(base.Rng.NextItem<MonsterModel>(FlyconidNormal._mediumSlimes).ToMutable(), null),
				new ValueTuple<MonsterModel, string>(ModelDb.Monster<Flyconid>().ToMutable(), null)
			});
		}

		// Token: 0x04002526 RID: 9510
		private static readonly MonsterModel[] _mediumSlimes = new MonsterModel[]
		{
			ModelDb.Monster<LeafSlimeM>(),
			ModelDb.Monster<TwigSlimeM>()
		};
	}
}
