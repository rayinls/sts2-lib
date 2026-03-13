using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Afflictions;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000685 RID: 1669
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RingingPower : PowerModel
	{
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x060054E6 RID: 21734 RVA: 0x00225757 File Offset: 0x00223957
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0022575A File Offset: 0x0022395A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060054E8 RID: 21736 RVA: 0x00225760 File Offset: 0x00223960
		[NullableContext(2)]
		[return: Nullable(1)]
		public override async Task AfterApplied(Creature applier, CardModel cardSource)
		{
			IEnumerable<CardModel> allCards = base.Owner.Player.PlayerCombatState.AllCards;
			foreach (CardModel cardModel in allCards)
			{
				if (cardModel.Affliction == null)
				{
					await CardCmd.Afflict<Ringing>(cardModel, 1m);
				}
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x060054E9 RID: 21737 RVA: 0x002257A4 File Offset: 0x002239A4
		public override async Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Owner == base.Owner.Player)
			{
				if (card.Affliction == null)
				{
					await CardCmd.Afflict<Ringing>(card, 1m);
				}
			}
		}

		// Token: 0x060054EA RID: 21738 RVA: 0x002257F0 File Offset: 0x002239F0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x060054EB RID: 21739 RVA: 0x0022583C File Offset: 0x00223A3C
		public override Task AfterRemoved(Creature oldOwner)
		{
			Player player = oldOwner.Player;
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
				if (cardModel.Affliction is Ringing)
				{
					CardCmd.ClearAffliction(cardModel);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x060054EC RID: 21740 RVA: 0x002258C0 File Offset: 0x00223AC0
		public override bool ShouldPlay(CardModel card, AutoPlayType _)
		{
			return card.Owner.Creature != base.Owner || !(card.Affliction is Ringing) || !CombatManager.Instance.History.CardPlaysStarted.Any((CardPlayStartedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Owner.Creature == base.Owner);
		}
	}
}
