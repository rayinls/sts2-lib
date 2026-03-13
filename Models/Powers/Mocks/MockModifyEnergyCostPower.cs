using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006DA RID: 1754
	public sealed class MockModifyEnergyCostPower : PowerModel
	{
		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x060056F9 RID: 22265 RVA: 0x0022961B File Offset: 0x0022781B
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x0022961E File Offset: 0x0022781E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x00229621 File Offset: 0x00227821
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x00229624 File Offset: 0x00227824
		[NullableContext(1)]
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost + base.Amount;
			return true;
		}
	}
}
