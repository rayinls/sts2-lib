using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000661 RID: 1633
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NoBlockPower : PowerModel
	{
		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x00223CDA File Offset: 0x00221EDA
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x06005407 RID: 21511 RVA: 0x00223CDD File Offset: 0x00221EDD
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x00223CE0 File Offset: 0x00221EE0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00223CF4 File Offset: 0x00221EF4
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.Decrement(this);
			}
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00223D3F File Offset: 0x00221F3F
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (props.HasFlag(ValueProp.Unpowered))
			{
				return 1m;
			}
			if (cardSource == null)
			{
				return 1m;
			}
			return 0m;
		}
	}
}
