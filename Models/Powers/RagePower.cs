using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200067B RID: 1659
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RagePower : PowerModel
	{
		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x00224F7F File Offset: 0x0022317F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x00224F82 File Offset: 0x00223182
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x00224F85 File Offset: 0x00223185
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00224F98 File Offset: 0x00223198
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				if (cardPlay.Card.Type == CardType.Attack)
				{
					await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
				}
			}
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00224FE4 File Offset: 0x002231E4
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
