using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200051D RID: 1309
	public sealed class JeweledMask : RelicModel
	{
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06004C52 RID: 19538 RVA: 0x00215613 File Offset: 0x00213813
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004C53 RID: 19539 RVA: 0x00215618 File Offset: 0x00213818
		[NullableContext(1)]
		public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner)
			{
				if (combatState.RoundNumber <= 1)
				{
					IReadOnlyList<CardModel> cards = PileType.Draw.GetPile(player).Cards;
					List<CardModel> list = cards.Where((CardModel c) => c.Type == CardType.Power).ToList<CardModel>();
					if (list.Count != 0)
					{
						CardModel cardModel = player.RunState.Rng.CombatCardSelection.NextItem<CardModel>(list);
						base.Flash();
						cardModel.SetToFreeThisTurn();
						await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
					}
				}
			}
		}
	}
}
