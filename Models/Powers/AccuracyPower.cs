using System;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D3 RID: 1491
	public sealed class AccuracyPower : PowerModel
	{
		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x0021E974 File Offset: 0x0021CB74
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x060050F2 RID: 20722 RVA: 0x0021E977 File Offset: 0x0021CB77
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x0021E97C File Offset: 0x0021CB7C
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel card)
		{
			if (base.Owner != dealer)
			{
				return 0m;
			}
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			if (card == null)
			{
				return 0m;
			}
			if (!card.Tags.Contains(CardTag.Shiv))
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}
