using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F3 RID: 1523
	public sealed class ConsumingShadowPower : PowerModel
	{
		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x060051A6 RID: 20902 RVA: 0x0021FBFF File Offset: 0x0021DDFF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x060051A7 RID: 20903 RVA: 0x0021FC02 File Offset: 0x0021DE02
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060051A8 RID: 20904 RVA: 0x0021FC08 File Offset: 0x0021DE08
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				if (base.Owner.Player.PlayerCombatState.OrbQueue.Orbs.Count != 0)
				{
					for (int i = 0; i < base.Amount; i++)
					{
						await OrbCmd.EvokeLast(choiceContext, base.Owner.Player, true);
						await Cmd.Wait(0.25f, false);
					}
				}
			}
		}
	}
}
