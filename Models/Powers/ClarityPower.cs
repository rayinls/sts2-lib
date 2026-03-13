using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005EE RID: 1518
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ClarityPower : PowerModel
	{
		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06005188 RID: 20872 RVA: 0x0021F88F File Offset: 0x0021DA8F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06005189 RID: 20873 RVA: 0x0021F892 File Offset: 0x0021DA92
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600518A RID: 20874 RVA: 0x0021F895 File Offset: 0x0021DA95
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			return count + 1m;
		}

		// Token: 0x0600518B RID: 20875 RVA: 0x0021F8B4 File Offset: 0x0021DAB4
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Decrement(this);
			}
		}
	}
}
