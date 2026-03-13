using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000679 RID: 1657
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PyrePower : PowerModel
	{
		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x0600549E RID: 21662 RVA: 0x00224ECB File Offset: 0x002230CB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x0600549F RID: 21663 RVA: 0x00224ECE File Offset: 0x002230CE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x060054A0 RID: 21664 RVA: 0x00224ED1 File Offset: 0x002230D1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x00224EDE File Offset: 0x002230DE
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
