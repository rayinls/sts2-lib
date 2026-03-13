using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000690 RID: 1680
	[NullableContext(1)]
	[Nullable(0)]
	public class ShacklingPotionPower : TemporaryStrengthPower
	{
		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x0600552D RID: 21805 RVA: 0x00226092 File Offset: 0x00224292
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Potion<ShacklingPotion>();
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x0600552E RID: 21806 RVA: 0x00226099 File Offset: 0x00224299
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
