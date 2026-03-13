using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000613 RID: 1555
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DuplicationPower : PowerModel
	{
		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06005261 RID: 21089 RVA: 0x00221053 File Offset: 0x0021F253
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06005262 RID: 21090 RVA: 0x00221056 File Offset: 0x0021F256
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005263 RID: 21091 RVA: 0x00221059 File Offset: 0x0021F259
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return playCount;
			}
			return playCount + 1;
		}

		// Token: 0x06005264 RID: 21092 RVA: 0x00221074 File Offset: 0x0021F274
		public override async Task AfterModifyingCardPlayCount(CardModel card)
		{
			await PowerCmd.Decrement(this);
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x002210B8 File Offset: 0x0021F2B8
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
