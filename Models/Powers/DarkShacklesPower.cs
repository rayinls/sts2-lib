using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000604 RID: 1540
	[NullableContext(1)]
	[Nullable(0)]
	public class DarkShacklesPower : TemporaryStrengthPower
	{
		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x06005205 RID: 20997 RVA: 0x0022063B File Offset: 0x0021E83B
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<DarkShackles>();
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x06005206 RID: 20998 RVA: 0x00220642 File Offset: 0x0021E842
		protected override bool IsPositive
		{
			get
			{
				return false;
			}
		}
	}
}
