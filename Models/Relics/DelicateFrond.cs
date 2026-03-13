using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E0 RID: 1248
	public sealed class DelicateFrond : RelicModel
	{
		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x00212C0B File Offset: 0x00210E0B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x00212C10 File Offset: 0x00210E10
		[NullableContext(1)]
		public override async Task BeforeCombatStart()
		{
			base.Flash();
			while (base.Owner.HasOpenPotionSlots)
			{
				PotionModel potionModel = PotionFactory.CreateRandomPotionOutOfCombat(base.Owner, base.Owner.RunState.Rng.CombatPotionGeneration, null).ToMutable();
				PotionProcureResult potionProcureResult = await PotionCmd.TryToProcure(potionModel, base.Owner, -1);
				PotionProcureResult potionProcureResult2 = potionProcureResult;
				if (!potionProcureResult2.success)
				{
					break;
				}
			}
		}
	}
}
