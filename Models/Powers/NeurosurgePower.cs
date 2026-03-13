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
	// Token: 0x0200065F RID: 1631
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NeurosurgePower : PowerModel
	{
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060053F9 RID: 21497 RVA: 0x00223BC3 File Offset: 0x00221DC3
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x00223BC6 File Offset: 0x00221DC6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x00223BC9 File Offset: 0x00221DC9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x00223BD8 File Offset: 0x00221DD8
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Apply<DoomPower>(base.Owner, base.Amount, base.Owner, null, false);
			}
		}
	}
}
