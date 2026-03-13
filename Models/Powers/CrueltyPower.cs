using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005FD RID: 1533
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CrueltyPower : PowerModel
	{
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x060051DC RID: 20956 RVA: 0x00220171 File Offset: 0x0021E371
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x060051DD RID: 20957 RVA: 0x00220174 File Offset: 0x0021E374
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060051DE RID: 20958 RVA: 0x00220177 File Offset: 0x0021E377
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060051DF RID: 20959 RVA: 0x00220183 File Offset: 0x0021E383
		[NullableContext(2)]
		public decimal ModifyVulnerableMultiplier([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target == base.Owner)
			{
				return amount;
			}
			if (!props.IsPoweredAttack())
			{
				return amount;
			}
			return amount + base.Amount / 100m;
		}
	}
}
