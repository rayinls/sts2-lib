using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200087B RID: 2171
	public sealed class Vigorous : EnchantmentModel
	{
		// Token: 0x06006610 RID: 26128 RVA: 0x002532B0 File Offset: 0x002514B0
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Attack;
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06006611 RID: 26129 RVA: 0x002532B6 File Offset: 0x002514B6
		public override bool ShowAmount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06006612 RID: 26130 RVA: 0x002532B9 File Offset: 0x002514B9
		public override decimal EnchantDamageAdditive(decimal originalDamage, ValueProp props)
		{
			if (base.Status != EnchantmentStatus.Normal)
			{
				return 0m;
			}
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			return base.Amount;
		}

		// Token: 0x06006613 RID: 26131 RVA: 0x002532E2 File Offset: 0x002514E2
		[NullableContext(1)]
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card != base.Card)
			{
				return Task.CompletedTask;
			}
			base.Status = EnchantmentStatus.Disabled;
			return Task.CompletedTask;
		}
	}
}
