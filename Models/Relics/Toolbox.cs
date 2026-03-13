using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005AB RID: 1451
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Toolbox : RelicModel
	{
		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x06004FF7 RID: 20471 RVA: 0x0021C0EF File Offset: 0x0021A2EF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06004FF8 RID: 20472 RVA: 0x0021C0F2 File Offset: 0x0021A2F2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0021C100 File Offset: 0x0021A300
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				if (base.Owner.Creature.CombatState.RoundNumber == 1)
				{
					base.Flash();
					List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint), base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
					CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, list, base.Owner, false);
					CardModel cardModel2 = cardModel;
					if (cardModel2 != null)
					{
						await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Hand, true, CardPilePosition.Bottom);
					}
				}
			}
		}
	}
}
