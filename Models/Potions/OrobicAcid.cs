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
	// Token: 0x02000707 RID: 1799
	public sealed class OrobicAcid : PotionModel
	{
		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06005811 RID: 22545 RVA: 0x0022AAEB File Offset: 0x00228CEB
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Rare;
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06005812 RID: 22546 RVA: 0x0022AAEE File Offset: 0x00228CEE
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06005813 RID: 22547 RVA: 0x0022AAF1 File Offset: 0x00228CF1
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x06005814 RID: 22548 RVA: 0x0022AAF4 File Offset: 0x00228CF4
		[NullableContext(1)]
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			List<CardModel> list = new List<CardModel>();
			list.AddRange(CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Attack
				select c, 1, base.Owner.RunState.Rng.CombatCardGeneration));
			list.AddRange(CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Skill
				select c, 1, base.Owner.RunState.Rng.CombatCardGeneration));
			list.AddRange(CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Power
				select c, 1, base.Owner.RunState.Rng.CombatCardGeneration));
			foreach (CardModel cardModel in list)
			{
				cardModel.SetToFreeThisTurn();
			}
			await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
		}
	}
}
