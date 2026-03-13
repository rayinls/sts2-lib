using System;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C1 RID: 1729
	public sealed class TheHuntPower : PowerModel
	{
		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x0600566E RID: 22126 RVA: 0x00228770 File Offset: 0x00226970
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x0600566F RID: 22127 RVA: 0x00228773 File Offset: 0x00226973
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}
	}
}
