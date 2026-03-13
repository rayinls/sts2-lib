using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004DB RID: 1243
	public sealed class Crossbow : RelicModel
	{
		// Token: 0x17000D9B RID: 3483
		// (get) Token: 0x06004ACB RID: 19147 RVA: 0x00212A03 File Offset: 0x00210C03
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x00212A08 File Offset: 0x00210C08
		[NullableContext(1)]
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				IReadOnlyList<CardModel> readOnlyList = (from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
					where c.Type == CardType.Attack
					select c).ToList<CardModel>();
				if (readOnlyList.Count != 0)
				{
					base.Flash();
					List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, readOnlyList, 1, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
					foreach (CardModel cardModel in list)
					{
						cardModel.EnergyCost.SetThisTurnOrUntilPlayed(0, false);
					}
					await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}
