using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006D2 RID: 1746
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WasteAwayPower : PowerModel
	{
		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x060056D1 RID: 22225 RVA: 0x002292C3 File Offset: 0x002274C3
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x060056D2 RID: 22226 RVA: 0x002292C6 File Offset: 0x002274C6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x060056D3 RID: 22227 RVA: 0x002292C9 File Offset: 0x002274C9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x002292D6 File Offset: 0x002274D6
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner.Player)
			{
				return amount;
			}
			return amount - base.Amount;
		}
	}
}
