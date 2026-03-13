using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006AE RID: 1710
	public sealed class StrengthPower : PowerModel
	{
		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x060055D9 RID: 21977 RVA: 0x002272AB File Offset: 0x002254AB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x060055DA RID: 21978 RVA: 0x002272AE File Offset: 0x002254AE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x060055DB RID: 21979 RVA: 0x002272B1 File Offset: 0x002254B1
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060055DC RID: 21980 RVA: 0x002272B4 File Offset: 0x002254B4
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (base.Owner != dealer)
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
