using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000720 RID: 1824
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeprecatedPotionPool : PotionPoolModel
	{
		// Token: 0x1700148F RID: 5263
		// (get) Token: 0x060058B3 RID: 22707 RVA: 0x0022B6CB File Offset: 0x002298CB
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x060058B4 RID: 22708 RVA: 0x0022B6D2 File Offset: 0x002298D2
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return new <>z__ReadOnlySingleElementList<PotionModel>(ModelDb.Potion<DeprecatedPotion>());
		}
	}
}
