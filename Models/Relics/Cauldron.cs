using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D0 RID: 1232
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Cauldron : RelicModel
	{
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x00212387 File Offset: 0x00210587
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x06004A91 RID: 19089 RVA: 0x0021238A File Offset: 0x0021058A
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x0021238D File Offset: 0x0021058D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Potions", 5m));
			}
		}

		// Token: 0x06004A93 RID: 19091 RVA: 0x002123A4 File Offset: 0x002105A4
		public override async Task AfterObtained()
		{
			await RewardsCmd.OfferCustom(base.Owner, this.GenerateRewards());
		}

		// Token: 0x06004A94 RID: 19092 RVA: 0x002123E8 File Offset: 0x002105E8
		private List<Reward> GenerateRewards()
		{
			int intValue = base.DynamicVars["Potions"].IntValue;
			List<Reward> list = new List<Reward>();
			if (TestMode.IsOn)
			{
				for (int i = 0; i < intValue; i++)
				{
					list.Add(new PotionReward(Cauldron._testPotions[i % intValue].ToMutable(), base.Owner));
				}
			}
			else
			{
				for (int j = 0; j < intValue; j++)
				{
					list.Add(new PotionReward(base.Owner));
				}
			}
			return list;
		}

		// Token: 0x0400219C RID: 8604
		private const string _potionsKey = "Potions";

		// Token: 0x0400219D RID: 8605
		private static readonly PotionModel[] _testPotions = new PotionModel[]
		{
			ModelDb.Potion<FlexPotion>(),
			ModelDb.Potion<WeakPotion>(),
			ModelDb.Potion<VulnerablePotion>(),
			ModelDb.Potion<StrengthPotion>(),
			ModelDb.Potion<DexterityPotion>()
		};
	}
}
