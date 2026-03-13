using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D7 RID: 1495
	[NullableContext(1)]
	[Nullable(0)]
	public class AnticipatePower : TemporaryDexterityPower
	{
		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x0600510B RID: 20747 RVA: 0x0021EBA3 File Offset: 0x0021CDA3
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<Anticipate>();
			}
		}
	}
}
