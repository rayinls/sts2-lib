using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009E1 RID: 2529
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Normality : CardModel
	{
		// Token: 0x06006DE2 RID: 28130 RVA: 0x002621A7 File Offset: 0x002603A7
		public Normality()
			: base(-1, CardType.Curse, CardRarity.Curse, TargetType.None, true)
		{
		}

		// Token: 0x17001D9D RID: 7581
		// (get) Token: 0x06006DE3 RID: 28131 RVA: 0x002621B5 File Offset: 0x002603B5
		protected override bool ShouldGlowRedInternal
		{
			get
			{
				return this.ShouldPreventCardPlay;
			}
		}

		// Token: 0x17001D9E RID: 7582
		// (get) Token: 0x06006DE4 RID: 28132 RVA: 0x002621BD File Offset: 0x002603BD
		private bool ShouldPreventCardPlay
		{
			get
			{
				return this.CardsPlayedThisTurn >= 3;
			}
		}

		// Token: 0x17001D9F RID: 7583
		// (get) Token: 0x06006DE5 RID: 28133 RVA: 0x002621CB File Offset: 0x002603CB
		public override int MaxUpgradeLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001DA0 RID: 7584
		// (get) Token: 0x06006DE6 RID: 28134 RVA: 0x002621CE File Offset: 0x002603CE
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Unplayable);
			}
		}

		// Token: 0x17001DA1 RID: 7585
		// (get) Token: 0x06006DE7 RID: 28135 RVA: 0x002621D8 File Offset: 0x002603D8
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				DynamicVar[] array = new DynamicVar[3];
				array[0] = new CalculationBaseVar(3m);
				array[1] = new CalculationExtraVar(-1m);
				array[2] = new CalculatedVar("CalculatedCards").WithMultiplier((CardModel card, [Nullable(2)] Creature _) => Math.Min(3, ((Normality)card).CardsPlayedThisTurn));
				return new <>z__ReadOnlyArray<DynamicVar>(array);
			}
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x0026223C File Offset: 0x0026043C
		public override bool ShouldPlay(CardModel card, AutoPlayType _)
		{
			if (card.Owner != base.Owner)
			{
				return true;
			}
			CardPile pile = base.Pile;
			return pile == null || pile.Type != PileType.Hand || !this.ShouldPreventCardPlay;
		}

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x06006DE9 RID: 28137 RVA: 0x00262273 File Offset: 0x00260473
		private int CardsPlayedThisTurn
		{
			get
			{
				return CombatManager.Instance.History.CardPlaysStarted.Count((CardPlayStartedEntry e) => e.HappenedThisTurn(base.CombatState) && e.CardPlay.Card.Owner == base.Owner);
			}
		}

		// Token: 0x040025B7 RID: 9655
		private const int _numOfCardsPerTurn = 3;

		// Token: 0x040025B8 RID: 9656
		private const string _calculatedCardsKey = "CalculatedCards";
	}
}
