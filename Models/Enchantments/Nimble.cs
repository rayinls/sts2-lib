using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200086F RID: 2159
	public sealed class Nimble : EnchantmentModel
	{
		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x060065E4 RID: 26084 RVA: 0x00252E6F File Offset: 0x0025106F
		public override bool ShowAmount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060065E5 RID: 26085 RVA: 0x00252E72 File Offset: 0x00251072
		[NullableContext(1)]
		public override bool CanEnchant(CardModel card)
		{
			return base.CanEnchant(card) && card.GainsBlock;
		}

		// Token: 0x060065E6 RID: 26086 RVA: 0x00252E85 File Offset: 0x00251085
		public override decimal EnchantBlockAdditive(decimal originalBlock, ValueProp props)
		{
			if (!props.IsPoweredCardOrMonsterMoveBlock())
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}
