using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedFeelNoPainPower : PowerModel
	{
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x00221472 File Offset: 0x0021F672
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// (get) Token: 0x06005296 RID: 21142 RVA: 0x00221475 File Offset: 0x0021F675
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// (get) Token: 0x06005297 RID: 21143 RVA: 0x00221478 File Offset: 0x0021F678
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()) };
			}
		}

		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool _)
		{
			if (card.Owner.Creature == base.Owner)
			{
				await CreatureCmd.GainBlock(base.Owner, base.Amount, ValueProp.Unpowered, null, false);
			}
		}
	}
}
