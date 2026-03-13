using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E1 RID: 1505
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BiasedCognitionPower : PowerModel
	{
		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x0600513C RID: 20796 RVA: 0x0021EFE0 File Offset: 0x0021D1E0
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x0600513D RID: 20797 RVA: 0x0021EFE3 File Offset: 0x0021D1E3
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x0021EFE6 File Offset: 0x0021D1E6
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<FocusPower>());
			}
		}

		// Token: 0x0600513F RID: 20799 RVA: 0x0021EFF4 File Offset: 0x0021D1F4
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Apply<FocusPower>(base.Owner, -base.Amount, base.Owner, null, false);
			}
		}
	}
}
