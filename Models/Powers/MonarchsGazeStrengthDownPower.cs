using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200065B RID: 1627
	[NullableContext(1)]
	[Nullable(0)]
	public class MonarchsGazeStrengthDownPower : TemporaryStrengthPower
	{
		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060053E3 RID: 21475 RVA: 0x00223981 File Offset: 0x00221B81
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<MonarchsGaze>();
			}
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x060053E4 RID: 21476 RVA: 0x00223988 File Offset: 0x00221B88
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
