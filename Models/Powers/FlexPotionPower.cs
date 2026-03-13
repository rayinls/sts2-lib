using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000623 RID: 1571
	[NullableContext(1)]
	[Nullable(0)]
	public class FlexPotionPower : TemporaryStrengthPower
	{
		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060052B2 RID: 21170 RVA: 0x0022183B File Offset: 0x0021FA3B
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Potion<FlexPotion>();
			}
		}
	}
}
