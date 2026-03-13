using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B6 RID: 1718
	[NullableContext(1)]
	[Nullable(0)]
	public class SynchronizePower : TemporaryFocusPower
	{
		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06005610 RID: 22032 RVA: 0x00227AA0 File Offset: 0x00225CA0
		public override AbstractModel OriginModel
		{
			get
			{
				return ModelDb.Card<Synchronize>();
			}
		}
	}
}
