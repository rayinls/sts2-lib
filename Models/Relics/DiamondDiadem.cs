using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E3 RID: 1251
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DiamondDiadem : RelicModel
	{
		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x00212CF0 File Offset: 0x00210EF0
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06004AEB RID: 19179 RVA: 0x00212CFC File Offset: 0x00210EFC
		public override int DisplayAmount
		{
			get
			{
				return this._cardsPlayedThisTurn;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x00212D04 File Offset: 0x00210F04
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06004AED RID: 19181 RVA: 0x00212D07 File Offset: 0x00210F07
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("CardThreshold", 2m));
			}
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00212D20 File Offset: 0x00210F20
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

		// Token: 0x06004AEF RID: 19183 RVA: 0x00212D74 File Offset: 0x00210F74
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (!(this._cardsPlayedThisTurn > base.DynamicVars["CardThreshold"].BaseValue))
				{
					base.Flash();
					await PowerCmd.Apply<DiamondDiademPower>(base.Owner.Creature, 1m, base.Owner.Creature, null, false);
				}
			}
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00212DBF File Offset: 0x00210FBF
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				this._cardsPlayedThisTurn = 0;
				this.RefreshCounter();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00212DE6 File Offset: 0x00210FE6
		private void RefreshCounter()
		{
			base.Status = ((this._cardsPlayedThisTurn <= base.DynamicVars["CardThreshold"].BaseValue) ? RelicStatus.Active : RelicStatus.Normal);
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00212E1F File Offset: 0x0021101F
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this._cardsPlayedThisTurn = 0;
			base.Status = RelicStatus.Normal;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x040021A3 RID: 8611
		private const string _cardThresholdKey = "CardThreshold";

		// Token: 0x040021A4 RID: 8612
		private int _cardsPlayedThisTurn;
	}
}
