using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000642 RID: 1602
	[NullableContext(1)]
	[Nullable(0)]
	public class HotfixPower : TemporaryFocusPower
	{
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06005358 RID: 21336 RVA: 0x00222A93 File Offset: 0x00220C93
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<Hotfix>();
			}
		}
	}
}
