using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A7 RID: 1703
	public sealed class StampedePower : PowerModel
	{
		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x060055B3 RID: 21939 RVA: 0x00226EE3 File Offset: 0x002250E3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x060055B4 RID: 21940 RVA: 0x00226EE6 File Offset: 0x002250E6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055B5 RID: 21941 RVA: 0x00226EEC File Offset: 0x002250EC
		[NullableContext(1)]
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				CardPile hand = PileType.Hand.GetPile(base.Owner.Player);
				for (int i = 0; i < base.Amount; i++)
				{
					List<CardModel> list = hand.Cards.Where((CardModel c) => c.Type == CardType.Attack && !c.Keywords.Contains(CardKeyword.Unplayable)).ToList<CardModel>();
					CardModel cardModel = base.Owner.Player.RunState.Rng.Shuffle.NextItem<CardModel>(list);
					if (cardModel != null)
					{
						await CardCmd.AutoPlay(choiceContext, cardModel, null, AutoPlayType.Default, false, false);
					}
				}
			}
		}
	}
}
