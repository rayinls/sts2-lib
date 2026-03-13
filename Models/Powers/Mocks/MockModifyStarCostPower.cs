using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006DB RID: 1755
	public sealed class MockModifyStarCostPower : PowerModel
	{
		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x00229646 File Offset: 0x00227846
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x060056FF RID: 22271 RVA: 0x00229649 File Offset: 0x00227849
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06005700 RID: 22272 RVA: 0x0022964C File Offset: 0x0022784C
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x0022964F File Offset: 0x0022784F
		[NullableContext(1)]
		public override bool TryModifyStarCost(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost + base.Amount;
			return true;
		}
	}
}
