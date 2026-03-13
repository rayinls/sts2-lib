using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200068C RID: 1676
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SelfFormingClayPower : PowerModel
	{
		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x0600551B RID: 21787 RVA: 0x00225EED File Offset: 0x002240ED
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x0600551C RID: 21788 RVA: 0x00225EF0 File Offset: 0x002240F0
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x0600551D RID: 21789 RVA: 0x00225EF3 File Offset: 0x002240F3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()) };
			}
		}

		// Token: 0x0600551E RID: 21790 RVA: 0x00225F0C File Offset: 0x0022410C
		public override async Task AfterBlockCleared(Creature creature)
		{
			if (creature == base.Owner)
			{
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				await PowerCmd.Remove(this);
			}
		}
	}
}
