using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006CA RID: 1738
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TyrannyPower : PowerModel
	{
		// Token: 0x17001342 RID: 4930
		// (get) Token: 0x0600569A RID: 22170 RVA: 0x00228B4B File Offset: 0x00226D4B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x0600569B RID: 22171 RVA: 0x00228B4E File Offset: 0x00226D4E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x0600569C RID: 22172 RVA: 0x00228B51 File Offset: 0x00226D51
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x0600569D RID: 22173 RVA: 0x00228B5E File Offset: 0x00226D5E
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			return count + base.Amount;
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x00228B84 File Offset: 0x00226D84
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, player, new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, base.Amount), null, this);
				IEnumerable<CardModel> enumerable2 = enumerable;
				foreach (CardModel cardModel in enumerable2)
				{
					await CardCmd.Exhaust(choiceContext, cardModel, false, false);
				}
				IEnumerator<CardModel> enumerator = null;
			}
		}
	}
}
