using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000543 RID: 1347
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MusicBox : RelicModel
	{
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06004D4B RID: 19787 RVA: 0x002172D5 File Offset: 0x002154D5
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06004D4C RID: 19788 RVA: 0x002172D8 File Offset: 0x002154D8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06004D4D RID: 19789 RVA: 0x002172E5 File Offset: 0x002154E5
		// (set) Token: 0x06004D4E RID: 19790 RVA: 0x002172ED File Offset: 0x002154ED
		private bool WasUsedThisTurn
		{
			get
			{
				return this._wasUsedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._wasUsedThisTurn = value;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06004D4F RID: 19791 RVA: 0x002172FC File Offset: 0x002154FC
		// (set) Token: 0x06004D50 RID: 19792 RVA: 0x00217304 File Offset: 0x00215504
		[Nullable(2)]
		private CardModel CardBeingPlayed
		{
			[NullableContext(2)]
			get
			{
				return this._cardBeingPlayed;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._cardBeingPlayed = value;
			}
		}

		// Token: 0x06004D51 RID: 19793 RVA: 0x00217314 File Offset: 0x00215514
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (this.CardBeingPlayed != null)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (this.WasUsedThisTurn)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			this.CardBeingPlayed = cardPlay.Card;
			return Task.CompletedTask;
		}

		// Token: 0x06004D52 RID: 19794 RVA: 0x0021737C File Offset: 0x0021557C
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card == this.CardBeingPlayed)
			{
				base.Flash();
				CardModel cardModel = cardPlay.Card.CreateClone();
				CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Ethereal });
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
				this.WasUsedThisTurn = true;
				this.CardBeingPlayed = null;
			}
		}

		// Token: 0x06004D53 RID: 19795 RVA: 0x002173C7 File Offset: 0x002155C7
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.WasUsedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x002173EE File Offset: 0x002155EE
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.WasUsedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021E1 RID: 8673
		private bool _wasUsedThisTurn;

		// Token: 0x040021E2 RID: 8674
		[Nullable(2)]
		private CardModel _cardBeingPlayed;
	}
}
