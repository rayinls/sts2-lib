using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006AD RID: 1709
	public sealed class StratagemPower : PowerModel
	{
		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x060055D5 RID: 21973 RVA: 0x00227247 File Offset: 0x00225447
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x060055D6 RID: 21974 RVA: 0x0022724A File Offset: 0x0022544A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x00227250 File Offset: 0x00225450
		[NullableContext(1)]
		public override async Task AfterShuffle(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner.Player)
			{
				base.Flash();
				IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, (from c in PileType.Draw.GetPile(base.Owner.Player).Cards
					orderby c.Rarity, c.Id
					select c).ToList<CardModel>(), base.Owner.Player, new CardSelectorPrefs(base.SelectionScreenPrompt, base.Amount));
				IEnumerable<CardModel> enumerable2 = enumerable;
				foreach (CardModel cardModel in enumerable2)
				{
					await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
				}
				IEnumerator<CardModel> enumerator = null;
			}
		}
	}
}
