using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200068F RID: 1679
	[NullableContext(1)]
	[Nullable(0)]
	public class SetupStrikePower : TemporaryStrengthPower
	{
		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x0600552B RID: 21803 RVA: 0x00226083 File Offset: 0x00224283
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<SetupStrike>();
			}
		}
	}
}
