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
	// Token: 0x02000691 RID: 1681
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ShadowmeldPower : PowerModel
	{
		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06005530 RID: 21808 RVA: 0x002260A4 File Offset: 0x002242A4
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06005531 RID: 21809 RVA: 0x002260A7 File Offset: 0x002242A7
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06005532 RID: 21810 RVA: 0x002260AA File Offset: 0x002242AA
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005533 RID: 21811 RVA: 0x002260BC File Offset: 0x002242BC
		[NullableContext(2)]
		public override decimal ModifyBlockMultiplicative([Nullable(1)] Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
		{
			if (base.Owner != target)
			{
				return 1m;
			}
			return (decimal)Math.Pow(2.0, (double)base.Amount);
		}

		// Token: 0x06005534 RID: 21812 RVA: 0x002260E8 File Offset: 0x002242E8
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
