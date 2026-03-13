using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200063D RID: 1597
	[NullableContext(1)]
	[Nullable(0)]
	public class HelicalDartPower : TemporaryDexterityPower
	{
		// Token: 0x170011A1 RID: 4513
		// (get) Token: 0x0600533E RID: 21310 RVA: 0x002226FE File Offset: 0x002208FE
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Relic<HelicalDart>();
			}
		}
	}
}
