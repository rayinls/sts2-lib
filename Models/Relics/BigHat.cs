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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B5 RID: 1205
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BigHat : RelicModel
	{
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060049E4 RID: 18916 RVA: 0x00210EDE File Offset: 0x0020F0DE
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060049E5 RID: 18917 RVA: 0x00210EE1 File Offset: 0x0020F0E1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060049E6 RID: 18918 RVA: 0x00210EEE File Offset: 0x0020F0EE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x060049E7 RID: 18919 RVA: 0x00210EFC File Offset: 0x0020F0FC
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					IReadOnlyList<CardModel> readOnlyList = (from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
						where c.Keywords.Contains(CardKeyword.Ethereal)
						select c).ToList<CardModel>();
					if (readOnlyList.Count > 0)
					{
						base.Flash();
						List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, readOnlyList, base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
						base.Flash();
						await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
					}
				}
			}
		}
	}
}
