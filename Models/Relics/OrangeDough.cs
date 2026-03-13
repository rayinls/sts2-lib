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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200054D RID: 1357
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OrangeDough : RelicModel
	{
		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06004D8B RID: 19851 RVA: 0x00217963 File Offset: 0x00215B63
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06004D8C RID: 19852 RVA: 0x00217966 File Offset: 0x00215B66
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x00217974 File Offset: 0x00215B74
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
					base.Flash();
					await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}
