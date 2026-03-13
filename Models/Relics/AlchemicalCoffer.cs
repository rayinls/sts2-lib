using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004A8 RID: 1192
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AlchemicalCoffer : RelicModel
	{
		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06004984 RID: 18820 RVA: 0x002102B3 File Offset: 0x0020E4B3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06004985 RID: 18821 RVA: 0x002102B6 File Offset: 0x0020E4B6
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x002102B9 File Offset: 0x0020E4B9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("PotionSlots", 4m));
			}
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x002102D0 File Offset: 0x0020E4D0
		public override async Task AfterObtained()
		{
			int originalSlotCount = base.Owner.MaxPotionCount;
			await PlayerCmd.GainMaxPotionCount(base.DynamicVars["PotionSlots"].IntValue, base.Owner);
			List<PotionModel> potions = PotionFactory.CreateRandomPotionsOutOfCombat(base.Owner, base.DynamicVars["PotionSlots"].IntValue, base.Owner.RunState.Rng.CombatPotionGeneration, null);
			for (int i = 0; i < potions.Count; i++)
			{
				await PotionCmd.TryToProcure(potions[i].ToMutable(), base.Owner, originalSlotCount + i);
			}
		}

		// Token: 0x0400217F RID: 8575
		private const string _potionSlotsKey = "PotionSlots";
	}
}
