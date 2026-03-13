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
	// Token: 0x0200070F RID: 1807
	public sealed class PowerPotion : PotionModel
	{
		// Token: 0x17001445 RID: 5189
		// (get) Token: 0x06005844 RID: 22596 RVA: 0x0022AE8F File Offset: 0x0022908F
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x06005845 RID: 22597 RVA: 0x0022AE92 File Offset: 0x00229092
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x06005846 RID: 22598 RVA: 0x0022AE95 File Offset: 0x00229095
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x0022AE98 File Offset: 0x00229098
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Power
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
