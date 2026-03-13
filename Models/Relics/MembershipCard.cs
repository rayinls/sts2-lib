using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Merchant;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200053A RID: 1338
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MembershipCard : RelicModel
	{
		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x00216C94 File Offset: 0x00214E94
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Discount", 50m));
			}
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06004D1A RID: 19738 RVA: 0x00216CAC File Offset: 0x00214EAC
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x00216CB0 File Offset: 0x00214EB0
		public override decimal ModifyMerchantPrice(Player player, MerchantEntry entry, decimal originalPrice)
		{
			if (player != base.Owner)
			{
				return originalPrice;
			}
			if (!LocalContext.IsMe(base.Owner))
			{
				return originalPrice;
			}
			return originalPrice * (base.DynamicVars["Discount"].BaseValue / 100m);
		}

		// Token: 0x040021DB RID: 8667
		private const string _discountKey = "Discount";
	}
}
