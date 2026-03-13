using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000877 RID: 2167
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Spiral : EnchantmentModel
	{
		// Token: 0x06006602 RID: 26114 RVA: 0x0025316B File Offset: 0x0025136B
		public override bool CanEnchant(CardModel c)
		{
			return base.CanEnchant(c) && c.Rarity == CardRarity.Basic && (c.Tags.Contains(CardTag.Strike) || c.Tags.Contains(CardTag.Defend));
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06006603 RID: 26115 RVA: 0x0025319D File Offset: 0x0025139D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new IntVar("Times", 1m));
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06006604 RID: 26116 RVA: 0x002531B3 File Offset: 0x002513B3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.ReplayDynamic, new DynamicVar[] { base.DynamicVars["Times"] }));
			}
		}

		// Token: 0x06006605 RID: 26117 RVA: 0x002531DA File Offset: 0x002513DA
		public override int EnchantPlayCount(int originalPlayCount)
		{
			return originalPlayCount + base.DynamicVars["Times"].IntValue;
		}

		// Token: 0x0400254C RID: 9548
		private const string _timesKey = "Times";
	}
}
