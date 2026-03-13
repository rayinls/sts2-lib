using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200086D RID: 2157
	public sealed class Instinct : EnchantmentModel
	{
		// Token: 0x060065D8 RID: 26072 RVA: 0x00252DB7 File Offset: 0x00250FB7
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Attack;
		}

		// Token: 0x060065D9 RID: 26073 RVA: 0x00252DBD File Offset: 0x00250FBD
		[NullableContext(1)]
		public override bool CanEnchant(CardModel card)
		{
			return base.CanEnchant(card) && !card.Keywords.Contains(CardKeyword.Unplayable) && !card.EnergyCost.CostsX && card.EnergyCost.GetWithModifiers(CostModifiers.None) > 0;
		}

		// Token: 0x060065DA RID: 26074 RVA: 0x00252DF4 File Offset: 0x00250FF4
		protected override void OnEnchant()
		{
			base.Card.EnergyCost.UpgradeBy(-1);
		}
	}
}
