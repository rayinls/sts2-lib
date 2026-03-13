using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200056C RID: 1388
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PowerCell : RelicModel
	{
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x002195A3 File Offset: 0x002177A3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x002195A6 File Offset: 0x002177A6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x002195B4 File Offset: 0x002177B4
		public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					IEnumerable<CardModel> enumerable = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => !c.EnergyCost.CostsX && c.EnergyCost.GetWithModifiers(CostModifiers.Local) == 0).ToList<CardModel>().StableShuffle(base.Owner.RunState.Rng.CombatCardSelection)
						.Take(base.DynamicVars.Cards.IntValue);
					await CardPileCmd.Add(enumerable, PileType.Hand, CardPilePosition.Bottom, null, false);
				}
			}
		}
	}
}
