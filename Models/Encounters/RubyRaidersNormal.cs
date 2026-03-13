using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Encounters
{
	// Token: 0x0200083C RID: 2108
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RubyRaidersNormal : EncounterModel
	{
		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x060064C4 RID: 25796 RVA: 0x00251702 File Offset: 0x0024F902
		public override RoomType RoomType
		{
			get
			{
				return RoomType.Monster;
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x060064C5 RID: 25797 RVA: 0x00251705 File Offset: 0x0024F905
		public override IEnumerable<MonsterModel> AllPossibleMonsters
		{
			get
			{
				return RubyRaidersNormal._raiderValidCounts.Keys;
			}
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x00251714 File Offset: 0x0024F914
		[return: Nullable(new byte[] { 1, 0, 1, 2 })]
		protected override IReadOnlyList<ValueTuple<MonsterModel, string>> GenerateMonsters()
		{
			List<MonsterModel> currentRaiders = new List<MonsterModel>();
			List<ValueTuple<MonsterModel, string>> list = new List<ValueTuple<MonsterModel, string>>();
			Func<MonsterModel, bool> <>9__0;
			for (int i = 0; i < 3; i++)
			{
				IEnumerable<MonsterModel> keys = RubyRaidersNormal._raiderValidCounts.Keys;
				Func<MonsterModel, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (MonsterModel r) => currentRaiders.Count((MonsterModel c) => c == r) < RubyRaidersNormal._raiderValidCounts[r]);
				}
				List<MonsterModel> list2 = keys.Where(func).ToList<MonsterModel>();
				MonsterModel monsterModel = base.Rng.NextItem<MonsterModel>(list2);
				currentRaiders.Add(monsterModel);
				list.Add(new ValueTuple<MonsterModel, string>(monsterModel.ToMutable(), null));
			}
			return list;
		}

		// Token: 0x0400253E RID: 9534
		private static readonly Dictionary<MonsterModel, int> _raiderValidCounts = new Dictionary<MonsterModel, int>
		{
			{
				ModelDb.Monster<AxeRubyRaider>(),
				1
			},
			{
				ModelDb.Monster<AssassinRubyRaider>(),
				1
			},
			{
				ModelDb.Monster<BruteRubyRaider>(),
				1
			},
			{
				ModelDb.Monster<CrossbowRubyRaider>(),
				1
			},
			{
				ModelDb.Monster<TrackerRubyRaider>(),
				1
			}
		};
	}
}
