using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C7 RID: 1223
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BrilliantScarf : RelicModel
	{
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06004A4F RID: 19023 RVA: 0x00211BC3 File Offset: 0x0020FDC3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x00211BC6 File Offset: 0x0020FDC6
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress && this.CardsPlayedThisTurn < base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x00211BEE File Offset: 0x0020FDEE
		public override int DisplayAmount
		{
			get
			{
				return this.CardsPlayedThisTurn;
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06004A52 RID: 19026 RVA: 0x00211BF6 File Offset: 0x0020FDF6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(5));
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x06004A53 RID: 19027 RVA: 0x00211C03 File Offset: 0x0020FE03
		// (set) Token: 0x06004A54 RID: 19028 RVA: 0x00211C0B File Offset: 0x0020FE0B
		private int CardsPlayedThisTurn
		{
			get
			{
				return this._cardsPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._cardsPlayedThisTurn = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004A55 RID: 19029 RVA: 0x00211C20 File Offset: 0x0020FE20
		private void UpdateDisplay()
		{
			int intValue = base.DynamicVars.Cards.IntValue;
			base.Status = ((this.CardsPlayedThisTurn == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004A56 RID: 19030 RVA: 0x00211C59 File Offset: 0x0020FE59
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (!this.ShouldModifyCost(card))
			{
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x06004A57 RID: 19031 RVA: 0x00211C75 File Offset: 0x0020FE75
		public override bool TryModifyStarCost(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (!this.ShouldModifyCost(card))
			{
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x06004A58 RID: 19032 RVA: 0x00211C91 File Offset: 0x0020FE91
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.CardsPlayedThisTurn = 0;
			return Task.CompletedTask;
		}

		// Token: 0x06004A59 RID: 19033 RVA: 0x00211CB8 File Offset: 0x0020FEB8
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (!CombatManager.Instance.IsInProgress)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.IsAutoPlay)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			int cardsPlayedThisTurn = this.CardsPlayedThisTurn;
			this.CardsPlayedThisTurn = cardsPlayedThisTurn + 1;
			return Task.CompletedTask;
		}

		// Token: 0x06004A5A RID: 19034 RVA: 0x00211D13 File Offset: 0x0020FF13
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.CardsPlayedThisTurn = 0;
			return Task.CompletedTask;
		}

		// Token: 0x06004A5B RID: 19035 RVA: 0x00211D24 File Offset: 0x0020FF24
		private bool ShouldModifyCost(CardModel card)
		{
			if (!CombatManager.Instance.IsInProgress)
			{
				return false;
			}
			if (card.Owner.Creature != base.Owner.Creature)
			{
				return false;
			}
			if (this.CardsPlayedThisTurn != base.DynamicVars.Cards.BaseValue - 1m)
			{
				return false;
			}
			CardPile pile = card.Pile;
			PileType? pileType = ((pile != null) ? new PileType?(pile.Type) : null);
			if (pileType != null)
			{
				PileType valueOrDefault = pileType.GetValueOrDefault();
				if (valueOrDefault == PileType.Hand || valueOrDefault == PileType.Play)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002195 RID: 8597
		private int _cardsPlayedThisTurn;
	}
}
