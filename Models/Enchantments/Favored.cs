using System;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000869 RID: 2153
	public sealed class Favored : EnchantmentModel
	{
		// Token: 0x060065C2 RID: 26050 RVA: 0x00252BB4 File Offset: 0x00250DB4
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Attack;
		}

		// Token: 0x060065C3 RID: 26051 RVA: 0x00252BBA File Offset: 0x00250DBA
		public override decimal EnchantDamageMultiplicative(decimal originalDamage, ValueProp props)
		{
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return 2m;
		}
	}
}
