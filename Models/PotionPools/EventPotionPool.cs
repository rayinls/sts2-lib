using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000721 RID: 1825
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EventPotionPool : PotionPoolModel
	{
		// Token: 0x17001490 RID: 5264
		// (get) Token: 0x060058B6 RID: 22710 RVA: 0x0022B6E6 File Offset: 0x002298E6
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x060058B7 RID: 22711 RVA: 0x0022B6ED File Offset: 0x002298ED
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return new <>z__ReadOnlyArray<PotionModel>(new PotionModel[]
			{
				ModelDb.Potion<FoulPotion>(),
				ModelDb.Potion<GlowwaterPotion>()
			});
		}
	}
}
