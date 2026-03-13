using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000874 RID: 2164
	public sealed class SlumberingEssence : EnchantmentModel
	{
		// Token: 0x060065F9 RID: 26105 RVA: 0x0025306C File Offset: 0x0025126C
		[NullableContext(1)]
		public override Task BeforeFlush(PlayerChoiceContext choiceContext, Player player)
		{
			if (player != base.Card.Owner)
			{
				return Task.CompletedTask;
			}
			CardPile pile = base.Card.Pile;
			if (pile == null || pile.Type != PileType.Hand)
			{
				return Task.CompletedTask;
			}
			base.Card.EnergyCost.AddUntilPlayed(-1, false);
			return Task.CompletedTask;
		}
	}
}
