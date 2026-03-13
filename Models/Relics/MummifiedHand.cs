using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000542 RID: 1346
	public sealed class MummifiedHand : RelicModel
	{
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06004D48 RID: 19784 RVA: 0x002171EB File Offset: 0x002153EB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004D49 RID: 19785 RVA: 0x002171F0 File Offset: 0x002153F0
		[NullableContext(1)]
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!CombatManager.Instance.IsInProgress)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Power)
			{
				return Task.CompletedTask;
			}
			IReadOnlyList<CardModel> cards = PileType.Hand.GetPile(base.Owner).Cards;
			Rng combatCardSelection = base.Owner.RunState.Rng.CombatCardSelection;
			CardModel cardModel = combatCardSelection.NextItem<CardModel>(cards.Where((CardModel c) => c.CostsEnergyOrStars(false)));
			if (cardModel == null)
			{
				combatCardSelection.NextItem<CardModel>(cards.Where((CardModel c) => c.CostsEnergyOrStars(true)));
			}
			if (cardModel != null)
			{
				cardModel.SetToFreeThisTurn();
			}
			return Task.CompletedTask;
		}
	}
}
