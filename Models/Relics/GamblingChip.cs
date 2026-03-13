using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000507 RID: 1287
	public sealed class GamblingChip : RelicModel
	{
		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x0021489B File Offset: 0x00212A9B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x002148A0 File Offset: 0x00212AA0
		[NullableContext(1)]
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (base.Owner.Creature.CombatState.RoundNumber <= 1)
				{
					IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 999999999), null, this);
					List<CardModel> list = enumerable.ToList<CardModel>();
					if (list.Count != 0)
					{
						await CardCmd.DiscardAndDraw(choiceContext, list, list.Count);
					}
				}
			}
		}
	}
}
