using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000871 RID: 2161
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RoyallyApproved : EnchantmentModel
	{
		// Token: 0x060065EB RID: 26091 RVA: 0x00252EE4 File Offset: 0x002510E4
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType - CardType.Attack <= 1;
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x060065EC RID: 26092 RVA: 0x00252EFE File Offset: 0x002510FE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromKeyword(CardKeyword.Innate),
					HoverTipFactory.FromKeyword(CardKeyword.Retain)
				});
			}
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x00252F1D File Offset: 0x0025111D
		protected override void OnEnchant()
		{
			base.Card.AddKeyword(CardKeyword.Innate);
			base.Card.AddKeyword(CardKeyword.Retain);
		}
	}
}
