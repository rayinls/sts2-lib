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
	// Token: 0x02000608 RID: 1544
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DemonFormPower : PowerModel
	{
		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x06005219 RID: 21017 RVA: 0x002207DB File Offset: 0x0021E9DB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x0600521A RID: 21018 RVA: 0x002207DE File Offset: 0x0021E9DE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x0600521B RID: 21019 RVA: 0x002207E1 File Offset: 0x0021E9E1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x0600521C RID: 21020 RVA: 0x002207F0 File Offset: 0x0021E9F0
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
			}
		}
	}
}
