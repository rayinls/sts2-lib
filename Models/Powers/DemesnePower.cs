using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000606 RID: 1542
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DemesnePower : PowerModel
	{
		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x0600520F RID: 21007 RVA: 0x00220717 File Offset: 0x0021E917
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x06005210 RID: 21008 RVA: 0x0022071A File Offset: 0x0021E91A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x06005211 RID: 21009 RVA: 0x0022071D File Offset: 0x0021E91D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06005212 RID: 21010 RVA: 0x0022072A File Offset: 0x0021E92A
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			return count + base.Amount;
		}

		// Token: 0x06005213 RID: 21011 RVA: 0x0022074D File Offset: 0x0021E94D
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner.Player)
			{
				return amount;
			}
			return amount + base.Amount;
		}
	}
}
