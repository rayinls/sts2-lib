using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000637 RID: 1591
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HangPower : PowerModel
	{
		// Token: 0x17001191 RID: 4497
		// (get) Token: 0x0600531D RID: 21277 RVA: 0x0022243F File Offset: 0x0022063F
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x0600531E RID: 21278 RVA: 0x00222442 File Offset: 0x00220642
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x0600531F RID: 21279 RVA: 0x00222445 File Offset: 0x00220645
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Hang>(false));
			}
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x00222452 File Offset: 0x00220652
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!(cardSource is Hang))
			{
				return 1m;
			}
			return base.Amount;
		}
	}
}
