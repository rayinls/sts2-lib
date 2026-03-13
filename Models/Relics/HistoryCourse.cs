using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000516 RID: 1302
	public sealed class HistoryCourse : RelicModel
	{
		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06004C2A RID: 19498 RVA: 0x0021515B File Offset: 0x0021335B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x00215160 File Offset: 0x00213360
		[NullableContext(1)]
		public override async Task AfterPlayerTurnStartEarly(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (base.Owner.Creature.CombatState.RoundNumber != 1)
				{
					CardPlayFinishedEntry cardPlayFinishedEntry = CombatManager.Instance.History.CardPlaysFinished.LastOrDefault(delegate(CardPlayFinishedEntry e)
					{
						bool flag = e.CardPlay.Card.Owner == base.Owner && e.RoundNumber == base.Owner.Creature.CombatState.RoundNumber - 1;
						bool flag2 = flag;
						if (flag2)
						{
							CardType type = e.CardPlay.Card.Type;
							bool flag3 = type - CardType.Attack <= 1;
							flag2 = flag3;
						}
						return flag2 && !e.CardPlay.Card.IsDupe;
					});
					CardModel cardModel = ((cardPlayFinishedEntry != null) ? cardPlayFinishedEntry.CardPlay.Card : null);
					if (cardModel != null)
					{
						base.Flash();
						await CardCmd.AutoPlay(choiceContext, cardModel.CreateDupe(), null, AutoPlayType.Default, false, false);
					}
				}
			}
		}
	}
}
