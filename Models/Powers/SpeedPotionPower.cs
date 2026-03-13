using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A3 RID: 1699
	[NullableContext(1)]
	[Nullable(0)]
	public class SpeedPotionPower : TemporaryDexterityPower
	{
		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x060055A3 RID: 21923 RVA: 0x00226D6F File Offset: 0x00224F6F
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Potion<SpeedPotion>();
			}
		}
	}
}
