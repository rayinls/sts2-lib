using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Encounters;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x02000845 RID: 2117
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlimesWeak : EncounterModel
	{
		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x060064F3 RID: 25843 RVA: 0x00251B9C File Offset: 0x0024FD9C
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x060064F4 RID: 25844 RVA: 0x00251B9F File Offset: 0x0024FD9F
		public override IEnumerable<EncounterTag> Tags
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<EncounterTag>(EncounterTag.Slimes);
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x060064F5 RID: 25845 RVA: 0x00251BA7 File Offset: 0x0024FDA7
		public override bool IsWeak
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x060064F6 RID: 25846 RVA: 0x00251BAC File Offset: 0x0024FDAC
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				MonsterModel[] smallSlimes = SlimesWeak._smallSlimes;
				MonsterModel[] mediumSlimes = SlimesWeak._mediumSlimes;
				int num = 0;
				MonsterModel[] array = new MonsterModel[smallSlimes.Length + mediumSlimes.Length];
				ReadOnlySpan<MonsterModel> readOnlySpan = new ReadOnlySpan<MonsterModel>(smallSlimes);
				readOnlySpan.CopyTo(new Span<MonsterModel>(array).Slice(num, readOnlySpan.Length));
				num += readOnlySpan.Length;
				ReadOnlySpan<MonsterModel> readOnlySpan2 = new ReadOnlySpan<MonsterModel>(mediumSlimes);
				readOnlySpan2.CopyTo(new Span<MonsterModel>(array).Slice(num, readOnlySpan2.Length));
				num += readOnlySpan2.Length;
				return new <>z__ReadOnlyArray<MonsterModel>(array);
			}
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x00251C3C File Offset: 0x0024FE3C
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			List<ValueTuple<MonsterModel, string>> list = new List<ValueTuple<MonsterModel, string>>();
			List<MonsterModel> list2 = SlimesWeak._smallSlimes.ToList<MonsterModel>();
			MonsterModel monsterModel = base.Rng.NextItem<MonsterModel>(list2);
			list2.Remove(monsterModel);
			MonsterModel monsterModel2 = base.Rng.NextItem<MonsterModel>(list2);
			list.Add(new ValueTuple<MonsterModel, string>(monsterModel.ToMutable(), null));
			list.Add(new ValueTuple<MonsterModel, string>(base.Rng.NextItem<MonsterModel>(SlimesWeak._mediumSlimes).ToMutable(), null));
			list.Add(new ValueTuple<MonsterModel, string>(monsterModel2.ToMutable(), null));
			return list;
		}

		// Token: 0x0400253F RID: 9535
		private static readonly MonsterModel[] _smallSlimes = new MonsterModel[]
		{
			ModelDb.Monster<LeafSlimeS>(),
			ModelDb.Monster<TwigSlimeS>()
		};

		// Token: 0x04002540 RID: 9536
		private static readonly MonsterModel[] _mediumSlimes = new MonsterModel[]
		{
			ModelDb.Monster<LeafSlimeM>(),
			ModelDb.Monster<TwigSlimeM>()
		};
	}
}
