using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000684 RID: 1668
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RetainHandPower : PowerModel
	{
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x002256EA File Offset: 0x002238EA
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x060054E2 RID: 21730 RVA: 0x002256ED File Offset: 0x002238ED
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060054E3 RID: 21731 RVA: 0x002256F0 File Offset: 0x002238F0
		public override bool ShouldFlush(Player player)
		{
			return player != base.Owner.Player;
		}

		// Token: 0x060054E4 RID: 21732 RVA: 0x00225704 File Offset: 0x00223904
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Decrement(this);
			}
		}
	}
}
