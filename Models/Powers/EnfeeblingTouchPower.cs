using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000617 RID: 1559
	[NullableContext(1)]
	[Nullable(0)]
	public class EnfeeblingTouchPower : TemporaryStrengthPower
	{
		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06005275 RID: 21109 RVA: 0x0022121B File Offset: 0x0021F41B
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<EnfeeblingTouch>();
			}
		}

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x00221222 File Offset: 0x0021F422
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
