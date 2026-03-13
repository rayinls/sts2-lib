using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200087A RID: 2170
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TezcatarasEmber : EnchantmentModel
	{
		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x0600660D RID: 26125 RVA: 0x0025326B File Offset: 0x0025146B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Eternal));
			}
		}

		// Token: 0x0600660E RID: 26126 RVA: 0x00253278 File Offset: 0x00251478
		protected override void OnEnchant()
		{
			base.Card.EnergyCost.UpgradeBy(-base.Card.EnergyCost.GetWithModifiers(CostModifiers.None));
			base.Card.AddKeyword(CardKeyword.Eternal);
		}
	}
}
