using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000678 RID: 1656
	public sealed class PrepTimePower : PowerModel
	{
		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x0600549A RID: 21658 RVA: 0x00224E6F File Offset: 0x0022306F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x0600549B RID: 21659 RVA: 0x00224E72 File Offset: 0x00223072
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00224E78 File Offset: 0x00223078
		[NullableContext(1)]
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Apply<VigorPower>(base.Owner, base.Amount, base.Owner, null, false);
			}
		}
	}
}
