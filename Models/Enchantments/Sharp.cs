using System;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000872 RID: 2162
	public sealed class Sharp : EnchantmentModel
	{
		// Token: 0x060065EF RID: 26095 RVA: 0x00252F3F File Offset: 0x0025113F
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Attack;
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x060065F0 RID: 26096 RVA: 0x00252F45 File Offset: 0x00251145
		public override bool ShowAmount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060065F1 RID: 26097 RVA: 0x00252F48 File Offset: 0x00251148
		public override decimal EnchantDamageAdditive(decimal originalDamage, ValueProp props)
		{
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}
