using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005FE RID: 1534
	[NullableContext(1)]
	[Nullable(0)]
	public class CrushUnderPower : TemporaryStrengthPower
	{
		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060051E1 RID: 20961 RVA: 0x002201BF File Offset: 0x0021E3BF
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<CrushUnder>();
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060051E2 RID: 20962 RVA: 0x002201C6 File Offset: 0x0021E3C6
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
