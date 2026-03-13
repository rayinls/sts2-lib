using System;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005DC RID: 1500
	public sealed class BackAttackLeftPower : PowerModel
	{
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x0021EE3F File Offset: 0x0021D03F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06005127 RID: 20775 RVA: 0x0021EE42 File Offset: 0x0021D042
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}
	}
}
