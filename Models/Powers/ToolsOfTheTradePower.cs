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

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C6 RID: 1734
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToolsOfTheTradePower : PowerModel
	{
		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06005684 RID: 22148 RVA: 0x00228947 File Offset: 0x00226B47
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06005685 RID: 22149 RVA: 0x0022894A File Offset: 0x00226B4A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005686 RID: 22150 RVA: 0x0022894D File Offset: 0x00226B4D
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			return count + base.Amount;
		}

		// Token: 0x06005687 RID: 22151 RVA: 0x00228970 File Offset: 0x00226B70
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, player, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, base.Amount), null, this);
				List<CardModel> list = enumerable.ToList<CardModel>();
				if (list.Count != 0)
				{
					await CardCmd.Discard(choiceContext, list);
				}
			}
		}
	}
}
