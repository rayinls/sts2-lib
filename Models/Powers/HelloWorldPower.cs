using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200063E RID: 1598
	public sealed class HelloWorldPower : PowerModel
	{
		// Token: 0x170011A2 RID: 4514
		// (get) Token: 0x06005340 RID: 21312 RVA: 0x0022270D File Offset: 0x0022090D
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011A3 RID: 4515
		// (get) Token: 0x06005341 RID: 21313 RVA: 0x00222710 File Offset: 0x00220910
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x00222714 File Offset: 0x00220914
		[NullableContext(1)]
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				if (base.AmountOnTurnStart >= 1)
				{
					base.Flash();
					IEnumerable<CardModel> distinctForCombat = CardFactory.GetDistinctForCombat(base.Owner.Player, from c in base.Owner.Player.Character.CardPool.GetUnlockedCards(base.Owner.Player.UnlockState, base.Owner.Player.RunState.CardMultiplayerConstraint)
						where c.Rarity == CardRarity.Common
						select c, base.AmountOnTurnStart, base.Owner.Player.RunState.Rng.CombatCardGeneration);
					await CardPileCmd.AddGeneratedCardsToCombat(distinctForCombat, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}
