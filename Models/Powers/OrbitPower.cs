using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000667 RID: 1639
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OrbitPower : PowerModel
	{
		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x0022415B File Offset: 0x0022235B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600542A RID: 21546 RVA: 0x0022415E File Offset: 0x0022235E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x00224161 File Offset: 0x00222361
		public override int DisplayAmount
		{
			get
			{
				return 4 - base.GetInternalData<OrbitPower.Data>().energySpent % 4;
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600542C RID: 21548 RVA: 0x00224172 File Offset: 0x00222372
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x0600542D RID: 21549 RVA: 0x0022417F File Offset: 0x0022237F
		protected override object InitInternalData()
		{
			return new OrbitPower.Data();
		}

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x0600542E RID: 21550 RVA: 0x00224186 File Offset: 0x00222386
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x00224189 File Offset: 0x00222389
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(4));
			}
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00224198 File Offset: 0x00222398
		public override async Task AfterEnergySpent(CardModel card, int amount)
		{
			if (card.Owner.Creature == base.Owner)
			{
				if (amount > 0)
				{
					OrbitPower.Data data = base.GetInternalData<OrbitPower.Data>();
					data.energySpent += amount;
					int triggers = data.energySpent / 4 - data.triggerCount;
					if (triggers > 0)
					{
						base.Flash();
						await PlayerCmd.GainEnergy(base.Amount * triggers, base.Owner.Player);
						data.triggerCount += triggers;
					}
					base.InvokeDisplayAmountChanged();
				}
			}
		}

		// Token: 0x0400225F RID: 8799
		private const int _energyIncrement = 4;

		// Token: 0x02001A44 RID: 6724
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x0400670D RID: 26381
			public int energySpent;

			// Token: 0x0400670E RID: 26382
			public int triggerCount;
		}
	}
}
