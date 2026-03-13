using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000692 RID: 1682
	public sealed class ShadowStepPower : PowerModel
	{
		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06005536 RID: 21814 RVA: 0x0022613B File Offset: 0x0022433B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06005537 RID: 21815 RVA: 0x0022613E File Offset: 0x0022433E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00226144 File Offset: 0x00224344
		[NullableContext(1)]
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				await PowerCmd.Apply<DoubleDamagePower>(base.Owner, base.Amount, base.Owner, null, false);
				await PowerCmd.Remove(this);
			}
		}
	}
}
