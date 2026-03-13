using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000725 RID: 1829
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SharedPotionPool : PotionPoolModel
	{
		// Token: 0x17001497 RID: 5271
		// (get) Token: 0x060058C8 RID: 22728 RVA: 0x0022B7D3 File Offset: 0x002299D3
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x060058C9 RID: 22729 RVA: 0x0022B7DC File Offset: 0x002299DC
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return new <>z__ReadOnlyArray<PotionModel>(new PotionModel[]
			{
				ModelDb.Potion<AttackPotion>(),
				ModelDb.Potion<BeetleJuice>(),
				ModelDb.Potion<BlessingOfTheForge>(),
				ModelDb.Potion<BlockPotion>(),
				ModelDb.Potion<BottledPotential>(),
				ModelDb.Potion<Clarity>(),
				ModelDb.Potion<ColorlessPotion>(),
				ModelDb.Potion<CureAll>(),
				ModelDb.Potion<DexterityPotion>(),
				ModelDb.Potion<DistilledChaos>(),
				ModelDb.Potion<DropletOfPrecognition>(),
				ModelDb.Potion<Duplicator>(),
				ModelDb.Potion<EnergyPotion>(),
				ModelDb.Potion<EntropicBrew>(),
				ModelDb.Potion<ExplosiveAmpoule>(),
				ModelDb.Potion<FairyInABottle>(),
				ModelDb.Potion<FirePotion>(),
				ModelDb.Potion<FlexPotion>(),
				ModelDb.Potion<Fortifier>(),
				ModelDb.Potion<FruitJuice>(),
				ModelDb.Potion<FyshOil>(),
				ModelDb.Potion<GamblersBrew>(),
				ModelDb.Potion<GigantificationPotion>(),
				ModelDb.Potion<HeartOfIron>(),
				ModelDb.Potion<LiquidBronze>(),
				ModelDb.Potion<LiquidMemories>(),
				ModelDb.Potion<LuckyTonic>(),
				ModelDb.Potion<MazalethsGift>(),
				ModelDb.Potion<OrobicAcid>(),
				ModelDb.Potion<PotionOfBinding>(),
				ModelDb.Potion<PowderedDemise>(),
				ModelDb.Potion<PowerPotion>(),
				ModelDb.Potion<RadiantTincture>(),
				ModelDb.Potion<RegenPotion>(),
				ModelDb.Potion<ShacklingPotion>(),
				ModelDb.Potion<ShipInABottle>(),
				ModelDb.Potion<SkillPotion>(),
				ModelDb.Potion<SneckoOil>(),
				ModelDb.Potion<SpeedPotion>(),
				ModelDb.Potion<StableSerum>(),
				ModelDb.Potion<StrengthPotion>(),
				ModelDb.Potion<SwiftPotion>(),
				ModelDb.Potion<TouchOfInsanity>(),
				ModelDb.Potion<VulnerablePotion>(),
				ModelDb.Potion<WeakPotion>()
			});
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x0022B984 File Offset: 0x00229B84
		public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			List<PotionModel> list = base.AllPotions.ToList<PotionModel>();
			if (!unlockState.IsEpochRevealed<Potion1Epoch>())
			{
				foreach (PotionModel potionModel in Potion1Epoch.Potions)
				{
					list.Remove(potionModel);
				}
			}
			if (!unlockState.IsEpochRevealed<Potion2Epoch>())
			{
				foreach (PotionModel potionModel2 in Potion2Epoch.Potions)
				{
					list.Remove(potionModel2);
				}
			}
			return list;
		}
	}
}
