using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000652 RID: 1618
	public sealed class LoopPower : PowerModel
	{
		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x060053BA RID: 21434 RVA: 0x0022367F File Offset: 0x0022187F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x00223682 File Offset: 0x00221882
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x00223688 File Offset: 0x00221888
		[NullableContext(1)]
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				if (player.PlayerCombatState.OrbQueue.Orbs.Count != 0)
				{
					for (int i = 0; i < base.Amount; i++)
					{
						await OrbCmd.Passive(choiceContext, player.PlayerCombatState.OrbQueue.Orbs[0], null);
						await Cmd.Wait(0.25f, false);
					}
				}
			}
		}
	}
}
