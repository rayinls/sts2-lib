using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200086E RID: 2158
	public sealed class Momentum : EnchantmentModel
	{
		// Token: 0x060065DC RID: 26076 RVA: 0x00252E0F File Offset: 0x0025100F
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Attack;
		}

		// Token: 0x170019FF RID: 6655
		// (get) Token: 0x060065DD RID: 26077 RVA: 0x00252E15 File Offset: 0x00251015
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x060065DE RID: 26078 RVA: 0x00252E18 File Offset: 0x00251018
		public override bool ShowAmount
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x060065DF RID: 26079 RVA: 0x00252E1B File Offset: 0x0025101B
		// (set) Token: 0x060065E0 RID: 26080 RVA: 0x00252E23 File Offset: 0x00251023
		private int ExtraDamage
		{
			get
			{
				return this._extraDamage;
			}
			set
			{
				base.AssertMutable();
				this._extraDamage = value;
			}
		}

		// Token: 0x060065E1 RID: 26081 RVA: 0x00252E32 File Offset: 0x00251032
		[NullableContext(1)]
		public override Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] CardPlay cardPlay)
		{
			this.ExtraDamage += base.Amount;
			return Task.CompletedTask;
		}

		// Token: 0x060065E2 RID: 26082 RVA: 0x00252E4C File Offset: 0x0025104C
		public override decimal EnchantDamageAdditive(decimal originalDamage, ValueProp props)
		{
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			return this.ExtraDamage;
		}

		// Token: 0x0400254A RID: 9546
		private int _extraDamage;
	}
}
