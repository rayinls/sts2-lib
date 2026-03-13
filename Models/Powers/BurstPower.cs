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
	// Token: 0x020005E8 RID: 1512
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BurstPower : PowerModel
	{
		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x0021F40F File Offset: 0x0021D60F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06005166 RID: 20838 RVA: 0x0021F412 File Offset: 0x0021D612
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0021F415 File Offset: 0x0021D615
		public override int ModifyCardPlayCount(CardModel card, [Nullable(2)] Creature target, int playCount)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return playCount;
			}
			if (card.Type != CardType.Skill)
			{
				return playCount;
			}
			return playCount + 1;
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x0021F43C File Offset: 0x0021D63C
		public override async Task AfterModifyingCardPlayCount(CardModel card)
		{
			await PowerCmd.Decrement(this);
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x0021F480 File Offset: 0x0021D680
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
