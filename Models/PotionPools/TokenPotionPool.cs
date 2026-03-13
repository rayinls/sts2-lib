using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000727 RID: 1831
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TokenPotionPool : PotionPoolModel
	{
		// Token: 0x1700149A RID: 5274
		// (get) Token: 0x060058D1 RID: 22737 RVA: 0x0022BA83 File Offset: 0x00229C83
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x0022BA8A File Offset: 0x00229C8A
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return new <>z__ReadOnlySingleElementList<PotionModel>(ModelDb.Potion<PotionShapedRock>());
		}
	}
}
