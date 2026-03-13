using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200061E RID: 1566
	[NullableContext(1)]
	[Nullable(0)]
	public class FeedingFrenzyPower : TemporaryStrengthPower
	{
		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06005293 RID: 21139 RVA: 0x00221463 File Offset: 0x0021F663
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<FeedingFrenzy>();
			}
		}
	}
}
