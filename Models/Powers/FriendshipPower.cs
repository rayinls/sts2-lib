using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200062D RID: 1581
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FriendshipPower : PowerModel
	{
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x060052E0 RID: 21216 RVA: 0x00221D47 File Offset: 0x0021FF47
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x060052E1 RID: 21217 RVA: 0x00221D4A File Offset: 0x0021FF4A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x060052E2 RID: 21218 RVA: 0x00221D4D File Offset: 0x0021FF4D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x00221D5A File Offset: 0x0021FF5A
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
