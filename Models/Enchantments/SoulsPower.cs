using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000875 RID: 2165
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoulsPower : EnchantmentModel
	{
		// Token: 0x060065FB RID: 26107 RVA: 0x002530D1 File Offset: 0x002512D1
		public override bool CanEnchant(CardModel card)
		{
			return base.CanEnchant(card) && card.Keywords.Contains(CardKeyword.Exhaust);
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x060065FC RID: 26108 RVA: 0x002530EF File Offset: 0x002512EF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x060065FD RID: 26109 RVA: 0x002530FC File Offset: 0x002512FC
		protected override void OnEnchant()
		{
			CardCmd.RemoveKeyword(base.Card, new CardKeyword[] { CardKeyword.Exhaust });
		}
	}
}
