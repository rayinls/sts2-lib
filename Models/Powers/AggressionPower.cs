using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D6 RID: 1494
	public sealed class AggressionPower : PowerModel
	{
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06005107 RID: 20743 RVA: 0x0021EB47 File Offset: 0x0021CD47
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06005108 RID: 20744 RVA: 0x0021EB4A File Offset: 0x0021CD4A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x0021EB50 File Offset: 0x0021CD50
		[NullableContext(1)]
		public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				CardPile pile = PileType.Discard.GetPile(base.Owner.Player);
				IEnumerable<CardModel> enumerable = pile.Cards.Where((CardModel c) => c.Type == CardType.Attack);
				IEnumerable<CardModel> enumerable2 = enumerable.ToList<CardModel>().UnstableShuffle(base.Owner.Player.RunState.Rng.CombatCardSelection).Take(base.Amount);
				foreach (CardModel card in enumerable2)
				{
					await CardPileCmd.Add(card, PileType.Hand, CardPilePosition.Bottom, null, false);
					if (card.IsUpgradable)
					{
						CardCmd.Upgrade(card, CardPreviewStyle.HorizontalLayout);
					}
					card = null;
				}
				IEnumerator<CardModel> enumerator = null;
			}
		}
	}
}
