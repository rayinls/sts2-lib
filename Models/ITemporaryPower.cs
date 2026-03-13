using System;
using System.Runtime.CompilerServices;

namespace MegaCrit.Sts2.Core.Models
{
	// Token: 0x02000499 RID: 1177
	[NullableContext(1)]
	public interface ITemporaryPower
	{
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x060047B4 RID: 18356
		AbstractModel OriginModel { get; }

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x060047B5 RID: 18357
		PowerModel InternallyAppliedPower { get; }

		// Token: 0x060047B6 RID: 18358
		void IgnoreNextInstance();
	}
}
