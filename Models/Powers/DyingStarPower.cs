using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000614 RID: 1556
	[NullableContext(1)]
	[Nullable(0)]
	public class DyingStarPower : TemporaryStrengthPower
	{
		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06005267 RID: 21095 RVA: 0x0022110B File Offset: 0x0021F30B
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<DyingStar>();
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06005268 RID: 21096 RVA: 0x00221112 File Offset: 0x0021F312
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
