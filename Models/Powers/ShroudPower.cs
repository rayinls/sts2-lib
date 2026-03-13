using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000695 RID: 1685
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShroudPower : PowerModel
	{
		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x0600554D RID: 21837 RVA: 0x0022641E File Offset: 0x0022461E
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x0600554E RID: 21838 RVA: 0x00226421 File Offset: 0x00224621
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x0600554F RID: 21839 RVA: 0x00226424 File Offset: 0x00224624
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06005550 RID: 21840 RVA: 0x00226430 File Offset: 0x00224630
		public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (applier == base.Owner)
			{
				if (power is DoomPower)
				{
					await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}
