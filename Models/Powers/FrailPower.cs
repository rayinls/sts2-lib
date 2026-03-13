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
	// Token: 0x02000629 RID: 1577
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FrailPower : PowerModel
	{
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x00221A1B File Offset: 0x0021FC1B
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060052CC RID: 21196 RVA: 0x00221A1E File Offset: 0x0021FC1E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x00221A21 File Offset: 0x0021FC21
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060052CE RID: 21198 RVA: 0x00221A33 File Offset: 0x0021FC33
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (base.Owner != target)
			{
				return 1m;
			}
			if (!props.IsPoweredCardOrMonsterMoveBlock())
			{
				return 1m;
			}
			return 0.75m;
		}

		// Token: 0x060052CF RID: 21199 RVA: 0x00221A60 File Offset: 0x0021FC60
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}
	}
}
