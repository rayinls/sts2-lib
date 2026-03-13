using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000867 RID: 2151
	public sealed class Corrupted : EnchantmentModel
	{
		// Token: 0x170019F7 RID: 6647
		// (get) Token: 0x060065BB RID: 26043 RVA: 0x00252B28 File Offset: 0x00250D28
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x00252B2B File Offset: 0x00250D2B
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Attack;
		}

		// Token: 0x060065BD RID: 26045 RVA: 0x00252B31 File Offset: 0x00250D31
		public override decimal EnchantDamageMultiplicative(decimal originalDamage, ValueProp props)
		{
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return 1.5m;
		}

		// Token: 0x060065BE RID: 26046 RVA: 0x00252B4C File Offset: 0x00250D4C
		[NullableContext(1)]
		public override async Task OnPlay(PlayerChoiceContext choiceContext, [Nullable(2)] CardPlay cardPlay)
		{
			await CreatureCmd.Damage(choiceContext, base.Card.Owner.Creature, 2m, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, base.Card);
		}

		// Token: 0x04002547 RID: 9543
		private const decimal _damageAmount = 2m;
	}
}
