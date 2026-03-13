using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000538 RID: 1336
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MeatCleaver : RelicModel
	{
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06004D0E RID: 19726 RVA: 0x00216B2B File Offset: 0x00214D2B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x00216B2E File Offset: 0x00214D2E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Cook, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x00216B41 File Offset: 0x00214D41
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			options.Add(new CookRestSiteOption(player));
			return true;
		}
	}
}
