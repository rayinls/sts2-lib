using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000568 RID: 1384
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pocketwatch : RelicModel
	{
		// Token: 0x17000F51 RID: 3921
		// (get) Token: 0x06004E5D RID: 20061 RVA: 0x00219143 File Offset: 0x00217343
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x00219146 File Offset: 0x00217346
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x00219152 File Offset: 0x00217352
		public override int DisplayAmount
		{
			get
			{
				return this._cardsPlayedThisTurn;
			}
		}

		// Token: 0x17000F54 RID: 3924
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x0021915A File Offset: 0x0021735A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("CardThreshold", 3m),
					new CardsVar(3)
				});
			}
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x00219184 File Offset: 0x00217384
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
			this._cardsPlayedThisTurn++;
			this.RefreshCounter();
			return Task.CompletedTask;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x002191D8 File Offset: 0x002173D8
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player.Creature.CombatState.RoundNumber == 1)
			{
				return count;
			}
			if (player != base.Owner)
			{
				return count;
			}
			if (this._cardsPlayedLastTurn > base.DynamicVars["CardThreshold"].BaseValue)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.BaseValue;
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x00219244 File Offset: 0x00217444
		public override Task AfterModifyingHandDraw()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x00219251 File Offset: 0x00217451
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				this._cardsPlayedLastTurn = this._cardsPlayedThisTurn;
				this._cardsPlayedThisTurn = 0;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x0021927E File Offset: 0x0021747E
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				this.RefreshCounter();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004E66 RID: 20070 RVA: 0x0021929E File Offset: 0x0021749E
		private void RefreshCounter()
		{
			base.Status = ((this._cardsPlayedThisTurn <= base.DynamicVars["CardThreshold"].BaseValue) ? RelicStatus.Active : RelicStatus.Normal);
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x002192D7 File Offset: 0x002174D7
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this._cardsPlayedThisTurn = 0;
			this._cardsPlayedLastTurn = 0;
			base.Status = RelicStatus.Normal;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x04002201 RID: 8705
		private const string _cardThresholdKey = "CardThreshold";

		// Token: 0x04002202 RID: 8706
		private int _cardsPlayedThisTurn;

		// Token: 0x04002203 RID: 8707
		private int _cardsPlayedLastTurn;
	}
}
