using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Afflictions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200069E RID: 1694
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SmoggyPower : PowerModel
	{
		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06005589 RID: 21897 RVA: 0x00226A27 File Offset: 0x00224C27
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x0600558A RID: 21898 RVA: 0x00226A2A File Offset: 0x00224C2A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x0600558B RID: 21899 RVA: 0x00226A30 File Offset: 0x00224C30
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Skill)
				{
					base.Flash();
					IEnumerable<CardModel> allCards = base.Owner.Player.PlayerCombatState.AllCards;
					foreach (CardModel cardModel in allCards)
					{
						if (cardModel.Type == CardType.Skill && cardModel.Affliction == null)
						{
							await CardCmd.Afflict<Smog>(cardModel, 1m);
						}
					}
					IEnumerator<CardModel> enumerator = null;
				}
			}
		}

		// Token: 0x0600558C RID: 21900 RVA: 0x00226A7C File Offset: 0x00224C7C
		public override async Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Owner == base.Owner.Player)
			{
				if (card.Affliction == null)
				{
					if (card.Type == CardType.Skill)
					{
						if (CombatManager.Instance.History.CardPlaysStarted.Any((CardPlayStartedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Type == CardType.Skill && e.CardPlay.Card.Owner.Creature == base.Owner))
						{
							await CardCmd.Afflict<Smog>(card, 1m);
						}
					}
				}
			}
		}

		// Token: 0x0600558D RID: 21901 RVA: 0x00226AC8 File Offset: 0x00224CC8
		public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != base.Owner.Side)
			{
				return Task.CompletedTask;
			}
			Player player = base.Owner.Player;
			IEnumerable<CardModel> enumerable;
			if (player == null)
			{
				enumerable = null;
			}
			else
			{
				PlayerCombatState playerCombatState = player.PlayerCombatState;
				enumerable = ((playerCombatState != null) ? playerCombatState.AllCards : null);
			}
			IEnumerable<CardModel> enumerable2 = enumerable ?? Array.Empty<CardModel>();
			foreach (CardModel cardModel in enumerable2)
			{
				if (cardModel.Affliction is Smog)
				{
					CardCmd.ClearAffliction(cardModel);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x0600558E RID: 21902 RVA: 0x00226B64 File Offset: 0x00224D64
		public override bool ShouldPlay(CardModel card, AutoPlayType _)
		{
			return card.Owner != base.Owner.Player || !(card.Affliction is Smog);
		}
	}
}
