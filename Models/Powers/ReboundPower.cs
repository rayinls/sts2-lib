using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000680 RID: 1664
	public sealed class ReboundPower : PowerModel
	{
		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x002254D7 File Offset: 0x002236D7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x002254DA File Offset: 0x002236DA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060054D1 RID: 21713 RVA: 0x002254DD File Offset: 0x002236DD
		public override ValueTuple<PileType, CardPilePosition> ModifyCardPlayResultPileTypeAndPosition([Nullable(1)] CardModel card, bool isAutoPlay, ResourceInfo resources, PileType pileType, CardPilePosition position)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			if (pileType != PileType.Discard)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			return new ValueTuple<PileType, CardPilePosition>(PileType.Draw, CardPilePosition.Top);
		}

		// Token: 0x060054D2 RID: 21714 RVA: 0x00225514 File Offset: 0x00223714
		[NullableContext(1)]
		public override async Task AfterModifyingCardPlayResultPileOrPosition(CardModel card, PileType pileType, CardPilePosition position)
		{
			if (card.Owner.Creature == base.Owner)
			{
				base.Flash();
				await PowerCmd.Decrement(this);
			}
		}

		// Token: 0x060054D3 RID: 21715 RVA: 0x00225560 File Offset: 0x00223760
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
