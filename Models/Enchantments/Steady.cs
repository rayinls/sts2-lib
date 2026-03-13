using System;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000878 RID: 2168
	public sealed class Steady : EnchantmentModel
	{
		// Token: 0x06006607 RID: 26119 RVA: 0x002531FB File Offset: 0x002513FB
		protected override void OnEnchant()
		{
			base.Card.AddKeyword(CardKeyword.Retain);
		}
	}
}
