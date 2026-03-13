using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000671 RID: 1649
	[NullableContext(1)]
	[Nullable(0)]
	public class PiercingWailPower : TemporaryStrengthPower
	{
		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x0600546B RID: 21611 RVA: 0x00224846 File Offset: 0x00222A46
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<PiercingWail>();
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x0022484D File Offset: 0x00222A4D
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
