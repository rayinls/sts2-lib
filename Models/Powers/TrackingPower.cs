using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C8 RID: 1736
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TrackingPower : PowerModel
	{
		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06005691 RID: 22161 RVA: 0x00228A67 File Offset: 0x00226C67
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700133E RID: 4926
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x00228A6A File Offset: 0x00226C6A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700133F RID: 4927
		// (get) Token: 0x06005693 RID: 22163 RVA: 0x00228A6D File Offset: 0x00226C6D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x00228A7C File Offset: 0x00226C7C
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (cardSource == null)
			{
				return 1m;
			}
			if (dealer != base.Owner && !base.Owner.Pets.Contains(dealer))
			{
				return 1m;
			}
			if (target == null || !target.HasPower<WeakPower>())
			{
				return 1m;
			}
			return base.Amount;
		}
	}
}
