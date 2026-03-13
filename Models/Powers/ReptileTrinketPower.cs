using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000683 RID: 1667
	[NullableContext(1)]
	[Nullable(0)]
	public class ReptileTrinketPower : TemporaryStrengthPower
	{
		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x002256DB File Offset: 0x002238DB
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Relic<ReptileTrinket>();
			}
		}
	}
}
