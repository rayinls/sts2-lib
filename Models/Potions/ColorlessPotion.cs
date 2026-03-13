using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x020006E8 RID: 1768
	public sealed class ColorlessPotion : PotionModel
	{
		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x0600574C RID: 22348 RVA: 0x00229C3F File Offset: 0x00227E3F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x0600574D RID: 22349 RVA: 0x00229C42 File Offset: 0x00227E42
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x0600574E RID: 22350 RVA: 0x00229C45 File Offset: 0x00227E45
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x00229C48 File Offset: 0x00227E48
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), 3, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
			CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, list, base.Owner, true);
			CardModel cardModel2 = cardModel;
			if (cardModel2 != null)
			{
				cardModel2.SetToFreeThisTurn();
				await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}
	}
}
