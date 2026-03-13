using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A6 RID: 1446
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheCourier : RelicModel
	{
		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x06004FDD RID: 20445 RVA: 0x0021BE3C File Offset: 0x0021A03C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x06004FDE RID: 20446 RVA: 0x0021BE3F File Offset: 0x0021A03F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Discount", 20m));
			}
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0021BE57 File Offset: 0x0021A057
		public override decimal ModifyMerchantPrice(Player player, MerchantEntry entry, decimal originalPrice)
		{
			if (player != base.Owner)
			{
				return originalPrice;
			}
			return originalPrice * (1m - base.DynamicVars["Discount"].BaseValue / 100m);
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x0021BE95 File Offset: 0x0021A095
		public override bool ShouldRefillMerchantEntry(MerchantEntry entry, Player player)
		{
			return player == base.Owner;
		}

		// Token: 0x0400222A RID: 8746
		private const string _discountKey = "Discount";
	}
}
