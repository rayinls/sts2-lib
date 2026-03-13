using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000653 RID: 1619
	public sealed class MachineLearningPower : PowerModel
	{
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x002236E3 File Offset: 0x002218E3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x002236E6 File Offset: 0x002218E6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x002236E9 File Offset: 0x002218E9
		[NullableContext(1)]
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			return count + base.Amount;
		}
	}
}
