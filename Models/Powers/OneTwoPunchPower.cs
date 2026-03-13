using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000666 RID: 1638
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OneTwoPunchPower : PowerModel
	{
		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06005423 RID: 21539 RVA: 0x00224097 File Offset: 0x00222297
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06005424 RID: 21540 RVA: 0x0022409A File Offset: 0x0022229A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x0022409D File Offset: 0x0022229D
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return playCount;
			}
			if (card.Type != CardType.Attack)
			{
				return playCount;
			}
			return playCount + 1;
		}

		// Token: 0x06005426 RID: 21542 RVA: 0x002240C4 File Offset: 0x002222C4
		public override async Task AfterModifyingCardPlayCount(CardModel card)
		{
			await PowerCmd.Decrement(this);
		}

		// Token: 0x06005427 RID: 21543 RVA: 0x00224108 File Offset: 0x00222308
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
