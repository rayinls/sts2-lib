using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D2 RID: 1490
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AccelerantPower : PowerModel
	{
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060050ED RID: 20717 RVA: 0x0021E95A File Offset: 0x0021CB5A
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060050EE RID: 20718 RVA: 0x0021E95D File Offset: 0x0021CB5D
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060050EF RID: 20719 RVA: 0x0021E960 File Offset: 0x0021CB60
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}
	}
}
