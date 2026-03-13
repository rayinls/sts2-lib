using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200060A RID: 1546
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DexterityPower : PowerModel
	{
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06005223 RID: 21027 RVA: 0x002208CB File Offset: 0x0021EACB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06005224 RID: 21028 RVA: 0x002208CE File Offset: 0x0021EACE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06005225 RID: 21029 RVA: 0x002208D1 File Offset: 0x0021EAD1
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06005226 RID: 21030 RVA: 0x002208D4 File Offset: 0x0021EAD4
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x002208E8 File Offset: 0x0021EAE8
		[NullableContext(2)]
		public override decimal ModifyBlockAdditive([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (cardSource != null)
			{
				if (cardSource.Owner.Creature != base.Owner)
				{
					return 0m;
				}
			}
			else if (base.Owner != target)
			{
				return 0m;
			}
			if (!props.IsPoweredCardOrMonsterMoveBlock())
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}
