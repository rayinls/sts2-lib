using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000682 RID: 1666
	public sealed class RegenPower : PowerModel
	{
		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x060054DA RID: 21722 RVA: 0x0022567F File Offset: 0x0022387F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x060054DB RID: 21723 RVA: 0x00225682 File Offset: 0x00223882
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x060054DC RID: 21724 RVA: 0x00225685 File Offset: 0x00223885
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00225688 File Offset: 0x00223888
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (!base.Owner.IsDead)
				{
					base.Flash();
					await CreatureCmd.Heal(base.Owner, base.Amount, true);
					await PowerCmd.Decrement(this);
				}
			}
		}
	}
}
