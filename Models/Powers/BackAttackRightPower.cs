using System;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005DD RID: 1501
	public sealed class BackAttackRightPower : PowerModel
	{
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06005129 RID: 20777 RVA: 0x0021EE4D File Offset: 0x0021D04D
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x0600512A RID: 20778 RVA: 0x0021EE50 File Offset: 0x0021D050
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}
	}
}
