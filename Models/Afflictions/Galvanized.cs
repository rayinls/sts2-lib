using System;

namespace MegaCrit.Sts2.Core.Models.Afflictions
{
	// Token: 0x02000ADB RID: 2779
	public sealed class Galvanized : AfflictionModel
	{
		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x06007361 RID: 29537 RVA: 0x0026D914 File Offset: 0x0026BB14
		public override bool IsStackable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x06007362 RID: 29538 RVA: 0x0026D917 File Offset: 0x0026BB17
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}
	}
}
