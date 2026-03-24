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
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedRagePower : PowerModel
	{
		// (get) Token: 0x060054A9 RID: 21673 RVA: 0x00224F7F File Offset: 0x0022317F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// (get) Token: 0x060054AA RID: 21674 RVA: 0x00224F82 File Offset: 0x00223182
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// (get) Token: 0x060054AB RID: 21675 RVA: 0x00224F85 File Offset: 0x00223185
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()) };
			}
		}

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

		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
