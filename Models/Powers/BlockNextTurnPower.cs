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
	// Token: 0x020005E4 RID: 1508
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlockNextTurnPower : PowerModel
	{
		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x0600514E RID: 20814 RVA: 0x0021F203 File Offset: 0x0021D403
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x0600514F RID: 20815 RVA: 0x0021F206 File Offset: 0x0021D406
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06005150 RID: 20816 RVA: 0x0021F209 File Offset: 0x0021D409
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x0021F21C File Offset: 0x0021D41C
		public override async Task AfterBlockCleared(Creature creature)
		{
			if (creature == base.Owner)
			{
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				await PowerCmd.Remove(this);
			}
		}
	}
}
