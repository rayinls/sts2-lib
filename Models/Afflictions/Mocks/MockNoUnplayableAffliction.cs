using System;

namespace MegaCrit.Sts2.Core.Models.Afflictions.Mocks
{
	// Token: 0x02000ADF RID: 2783
	public sealed class MockNoUnplayableAffliction : AfflictionModel
	{
		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x0600736C RID: 29548 RVA: 0x0026D9C4 File Offset: 0x0026BBC4
		public override bool CanAfflictUnplayableCards
		{
			get
			{
				return false;
			}
		}
	}
}
