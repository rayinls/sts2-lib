using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200064F RID: 1615
	public sealed class LeadershipPower : PowerModel
	{
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x002234A7 File Offset: 0x002216A7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x060053AD RID: 21421 RVA: 0x002234AA File Offset: 0x002216AA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x002234B0 File Offset: 0x002216B0
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (base.Owner == dealer)
			{
				return 0m;
			}
			if (base.Owner.Side != dealer.Side)
			{
				return 0m;
			}
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}
