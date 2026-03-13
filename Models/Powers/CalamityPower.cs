using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E9 RID: 1513
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CalamityPower : PowerModel
	{
		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600516B RID: 20843 RVA: 0x0021F4D3 File Offset: 0x0021D6D3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x0600516C RID: 20844 RVA: 0x0021F4D6 File Offset: 0x0021D6D6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0021F4D9 File Offset: 0x0021D6D9
		protected override object InitInternalData()
		{
			return new CalamityPower.Data();
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0021F4E0 File Offset: 0x0021D6E0
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<CalamityPower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0021F540 File Offset: 0x0021D740
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			int num;
			if (base.GetInternalData<CalamityPower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num))
			{
				List<CardModel> list = CardFactory.GetForCombat(base.Owner.Player, from c in base.Owner.Player.Character.CardPool.GetUnlockedCards(base.Owner.Player.UnlockState, base.Owner.Player.RunState.CardMultiplayerConstraint)
					where c.Type == CardType.Attack
					select c, base.Amount, base.Owner.Player.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
				foreach (CardModel cardModel in list)
				{
					await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
				}
				List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
			}
		}

		// Token: 0x020019B7 RID: 6583
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006466 RID: 25702
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}
