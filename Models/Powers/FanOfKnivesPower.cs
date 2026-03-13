using System;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200061C RID: 1564
	public sealed class FanOfKnivesPower : PowerModel
	{
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600528A RID: 21130 RVA: 0x002213D7 File Offset: 0x0021F5D7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x0600528B RID: 21131 RVA: 0x002213DA File Offset: 0x0021F5DA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}
	}
}
