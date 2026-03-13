using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000605 RID: 1541
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DebilitatePower : PowerModel
	{
		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x06005208 RID: 21000 RVA: 0x0022064D File Offset: 0x0021E84D
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06005209 RID: 21001 RVA: 0x00220650 File Offset: 0x0021E850
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x0600520A RID: 21002 RVA: 0x00220653 File Offset: 0x0021E853
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<VulnerablePower>(),
					HoverTipFactory.FromPower<WeakPower>()
				});
			}
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x00220670 File Offset: 0x0021E870
		[NullableContext(2)]
		public decimal ModifyVulnerableMultiplier([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return amount;
			}
			if (!props.IsPoweredAttack())
			{
				return amount;
			}
			return amount + (amount - 1m);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x00220698 File Offset: 0x0021E898
		[NullableContext(2)]
		public decimal ModifyWeakMultiplier([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (dealer != base.Owner)
			{
				return amount;
			}
			if (!props.IsPoweredAttack())
			{
				return amount;
			}
			return amount - (1m - amount);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x002206C4 File Offset: 0x0021E8C4
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Decrement(this);
			}
		}
	}
}
