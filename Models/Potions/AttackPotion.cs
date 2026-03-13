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
	// Token: 0x020006E0 RID: 1760
	public sealed class AttackPotion : PotionModel
	{
		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x0600571C RID: 22300 RVA: 0x00229837 File Offset: 0x00227A37
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x0600571D RID: 22301 RVA: 0x0022983A File Offset: 0x00227A3A
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x0600571E RID: 22302 RVA: 0x0022983D File Offset: 0x00227A3D
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x00229840 File Offset: 0x00227A40
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Attack
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
