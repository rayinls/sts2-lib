using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000611 RID: 1553
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DrawCardsNextTurnPower : PowerModel
	{
		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06005257 RID: 21079 RVA: 0x00220F5B File Offset: 0x0021F15B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06005258 RID: 21080 RVA: 0x00220F5E File Offset: 0x0021F15E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005259 RID: 21081 RVA: 0x00220F61 File Offset: 0x0021F161
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			if (base.AmountOnTurnStart == 0)
			{
				return count;
			}
			return count + base.Amount;
		}

		// Token: 0x0600525A RID: 21082 RVA: 0x00220F90 File Offset: 0x0021F190
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				if (base.AmountOnTurnStart != 0)
				{
					await PowerCmd.Remove(this);
				}
			}
		}
	}
}
