using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005FF RID: 1535
	public sealed class CuriousPower : PowerModel
	{
		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060051E4 RID: 20964 RVA: 0x002201D1 File Offset: 0x0021E3D1
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060051E5 RID: 20965 RVA: 0x002201D4 File Offset: 0x0021E3D4
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060051E6 RID: 20966 RVA: 0x002201D8 File Offset: 0x0021E3D8
		[NullableContext(1)]
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Power)
			{
				return false;
			}
			if (originalCost <= 0m)
			{
				return false;
			}
			modifiedCost = originalCost - base.Amount;
			if (modifiedCost < 0m)
			{
				modifiedCost = 0m;
			}
			return true;
		}
	}
}
