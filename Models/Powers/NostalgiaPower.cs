using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000663 RID: 1635
	public sealed class NostalgiaPower : PowerModel
	{
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06005411 RID: 21521 RVA: 0x00223DEF File Offset: 0x00221FEF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06005412 RID: 21522 RVA: 0x00223DF2 File Offset: 0x00221FF2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005413 RID: 21523 RVA: 0x00223DF8 File Offset: 0x00221FF8
		public override ValueTuple<PileType, CardPilePosition> ModifyCardPlayResultPileTypeAndPosition([Nullable(1)] CardModel card, bool isAutoPlay, ResourceInfo resources, PileType pileType, CardPilePosition position)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			CardType type = card.Type;
			if (type - CardType.Attack > 1)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			if (pileType != PileType.Discard)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			int num = CombatManager.Instance.History.CardPlaysStarted.Count(delegate(CardPlayStartedEntry e)
			{
				bool flag = e.HappenedThisTurn(base.CombatState);
				bool flag2 = flag;
				if (flag2)
				{
					CardType type2 = e.CardPlay.Card.Type;
					bool flag3 = type2 - CardType.Attack <= 1;
					flag2 = flag3;
				}
				return flag2 && e.CardPlay.Card.Owner == base.Owner.Player;
			});
			if (num >= base.Amount)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			return new ValueTuple<PileType, CardPilePosition>(PileType.Draw, CardPilePosition.Top);
		}

		// Token: 0x06005414 RID: 21524 RVA: 0x00223E8C File Offset: 0x0022208C
		[NullableContext(1)]
		public override Task AfterModifyingCardPlayResultPileOrPosition(CardModel card, PileType pileType, CardPilePosition position)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
