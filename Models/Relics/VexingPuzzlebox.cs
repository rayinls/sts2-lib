using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005BD RID: 1469
	public sealed class VexingPuzzlebox : RelicModel
	{
		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06005086 RID: 20614 RVA: 0x0021D1E7 File Offset: 0x0021B3E7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06005087 RID: 20615 RVA: 0x0021D1EC File Offset: 0x0021B3EC
		[NullableContext(1)]
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (base.Owner.Creature.CombatState.RoundNumber == 1)
				{
					base.Flash();
					CardModel cardModel = CardFactory.GetDistinctForCombat(base.Owner, base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), 1, base.Owner.RunState.Rng.CombatCardGeneration).First<CardModel>();
					cardModel.EnergyCost.SetThisCombat(0, false);
					await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}
