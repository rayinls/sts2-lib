using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200064D RID: 1613
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JugglingPower : PowerModel
	{
		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600539C RID: 21404 RVA: 0x002232B3 File Offset: 0x002214B3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x002232B6 File Offset: 0x002214B6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600539E RID: 21406 RVA: 0x002232B9 File Offset: 0x002214B9
		protected override object InitInternalData()
		{
			return new JugglingPower.Data();
		}

		// Token: 0x0600539F RID: 21407 RVA: 0x002232C0 File Offset: 0x002214C0
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			base.GetInternalData<JugglingPower.Data>().attacksPlayedThisTurn = CombatManager.Instance.History.CardPlaysStarted.Count((CardPlayStartedEntry e) => e.CardPlay.Card.Type == CardType.Attack && e.CardPlay.Card.Owner.Creature == base.Owner && e.HappenedThisTurn(base.CombatState));
			return Task.CompletedTask;
		}

		// Token: 0x060053A0 RID: 21408 RVA: 0x002232F4 File Offset: 0x002214F4
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				if (cardPlay.Card.Type == CardType.Attack)
				{
					base.GetInternalData<JugglingPower.Data>().attacksPlayedThisTurn++;
					if (base.GetInternalData<JugglingPower.Data>().attacksPlayedThisTurn == 3)
					{
						base.Flash();
						for (int i = 0; i < base.Amount; i++)
						{
							CardModel cardModel = cardPlay.Card.CreateClone();
							await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
						}
					}
				}
			}
		}

		// Token: 0x060053A1 RID: 21409 RVA: 0x0022333F File Offset: 0x0022153F
		public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.GetInternalData<JugglingPower.Data>().attacksPlayedThisTurn = 0;
			}
			return Task.CompletedTask;
		}

		// Token: 0x02001A2B RID: 6699
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006692 RID: 26258
			public int attacksPlayedThisTurn;
		}
	}
}
