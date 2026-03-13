using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200053E RID: 1342
	public sealed class MiniatureTent : RelicModel
	{
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06004D33 RID: 19763 RVA: 0x00216FF9 File Offset: 0x002151F9
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x00216FFC File Offset: 0x002151FC
		[NullableContext(1)]
		public override bool ShouldDisableRemainingRestSiteOptions(Player player)
		{
			if (player != base.Owner)
			{
				return true;
			}
			base.Flash();
			return false;
		}
	}
}
