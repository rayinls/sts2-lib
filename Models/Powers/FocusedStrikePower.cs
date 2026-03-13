using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000625 RID: 1573
	[NullableContext(1)]
	[Nullable(0)]
	public class FocusedStrikePower : TemporaryFocusPower
	{
		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x060052BC RID: 21180 RVA: 0x00221917 File Offset: 0x0021FB17
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<FocusedStrike>();
			}
		}
	}
}
