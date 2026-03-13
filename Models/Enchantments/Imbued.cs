using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x0200086C RID: 2156
	public sealed class Imbued : EnchantmentModel
	{
		// Token: 0x060065D3 RID: 26067 RVA: 0x00252D4F File Offset: 0x00250F4F
		public override bool CanEnchantCardType(CardType cardType)
		{
			return cardType == CardType.Skill;
		}

		// Token: 0x170019FD RID: 6653
		// (get) Token: 0x060065D4 RID: 26068 RVA: 0x00252D55 File Offset: 0x00250F55
		public override bool ShouldStartAtBottomOfDrawPile
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170019FE RID: 6654
		// (get) Token: 0x060065D5 RID: 26069 RVA: 0x00252D58 File Offset: 0x00250F58
		public override bool ShowAmount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060065D6 RID: 26070 RVA: 0x00252D5C File Offset: 0x00250F5C
		[NullableContext(1)]
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Card.Owner)
			{
				if (base.Card.CombatState.RoundNumber == 1)
				{
					await CardCmd.AutoPlay(choiceContext, base.Card, null, AutoPlayType.Default, false, false);
				}
			}
		}
	}
}
