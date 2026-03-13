using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006D4 RID: 1748
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WellLaidPlansPower : PowerModel
	{
		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x060056DC RID: 22236 RVA: 0x002293FF File Offset: 0x002275FF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x00229402 File Offset: 0x00227602
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060056DE RID: 22238 RVA: 0x00229408 File Offset: 0x00227608
		public override async Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				if (Hook.ShouldFlush(player.Creature.CombatState, player))
				{
					CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 0, base.Amount);
					IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner.Player, cardSelectorPrefs, new Func<CardModel, bool>(this.RetainFilter), this);
					IEnumerable<CardModel> enumerable2 = enumerable;
					List<CardModel> list = enumerable2.ToList<CardModel>();
					if (list.Count != 0)
					{
						foreach (CardModel cardModel in list)
						{
							cardModel.GiveSingleTurnRetain();
						}
					}
				}
			}
		}

		// Token: 0x060056DF RID: 22239 RVA: 0x0022945B File Offset: 0x0022765B
		private bool RetainFilter(CardModel card)
		{
			return !card.ShouldRetainThisTurn;
		}
	}
}
