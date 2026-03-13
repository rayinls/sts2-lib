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
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F1 RID: 1521
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ConquerorPower : PowerModel
	{
		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x0600519B RID: 20891 RVA: 0x0021FAB2 File Offset: 0x0021DCB2
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600519C RID: 20892 RVA: 0x0021FAB5 File Offset: 0x0021DCB5
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x0600519D RID: 20893 RVA: 0x0021FAB8 File Offset: 0x0021DCB8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<SovereignBlade>(false);
			}
		}

		// Token: 0x0600519E RID: 20894 RVA: 0x0021FAC0 File Offset: 0x0021DCC0
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!(cardSource is SovereignBlade))
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (target != base.Owner)
			{
				return 1m;
			}
			return 2m;
		}

		// Token: 0x0600519F RID: 20895 RVA: 0x0021FAF4 File Offset: 0x0021DCF4
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}
	}
}
