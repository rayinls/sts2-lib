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

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000714 RID: 1812
	public sealed class SkillPotion : PotionModel
	{
		// Token: 0x1700145D RID: 5213
		// (get) Token: 0x06005866 RID: 22630 RVA: 0x0022B0F3 File Offset: 0x002292F3
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x06005867 RID: 22631 RVA: 0x0022B0F6 File Offset: 0x002292F6
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700145F RID: 5215
		// (get) Token: 0x06005868 RID: 22632 RVA: 0x0022B0F9 File Offset: 0x002292F9
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x06005869 RID: 22633 RVA: 0x0022B0FC File Offset: 0x002292FC
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Skill
				select c, 3, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
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
