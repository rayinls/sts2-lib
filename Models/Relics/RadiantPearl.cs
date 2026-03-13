using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000574 RID: 1396
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RadiantPearl : RelicModel
	{
		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x002199CB File Offset: 0x00217BCB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x002199CE File Offset: 0x00217BCE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x002199DB File Offset: 0x00217BDB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.ForEnergy(this),
					HoverTipFactory.FromCard<Luminesce>(false)
				});
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x002199FC File Offset: 0x00217BFC
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				if (combatState.RoundNumber == 1)
				{
					List<CardModel> list = new List<CardModel>();
					for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
					{
						list.Add(base.Owner.Creature.CombatState.CreateCard<Luminesce>(base.Owner));
					}
					await CardPileCmd.AddGeneratedCardsToCombat(list, PileType.Hand, true, CardPilePosition.Bottom);
				}
			}
		}
	}
}
