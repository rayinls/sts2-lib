using System;
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
	// Token: 0x020005FB RID: 1531
	public sealed class CreativeAiPower : PowerModel
	{
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x060051D1 RID: 20945 RVA: 0x00220053 File Offset: 0x0021E253
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x060051D2 RID: 20946 RVA: 0x00220056 File Offset: 0x0021E256
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060051D3 RID: 20947 RVA: 0x0022005C File Offset: 0x0021E25C
		[NullableContext(1)]
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				for (int i = 0; i < base.Amount; i++)
				{
					CardModel cardModel = CardFactory.GetDistinctForCombat(player, from c in player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
						where c.Type == CardType.Power
						select c, 1, player.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
					if (cardModel != null)
					{
						await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
					}
				}
			}
		}
	}
}
