using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200068B RID: 1675
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SeekingEdgePower : PowerModel
	{
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06005517 RID: 21783 RVA: 0x00225ED8 File Offset: 0x002240D8
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06005518 RID: 21784 RVA: 0x00225EDB File Offset: 0x002240DB
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06005519 RID: 21785 RVA: 0x00225EDE File Offset: 0x002240DE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}
	}
}
