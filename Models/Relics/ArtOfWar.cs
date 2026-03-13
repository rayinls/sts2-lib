using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004AD RID: 1197
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ArtOfWar : RelicModel
	{
		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x060049A9 RID: 18857 RVA: 0x002107E6 File Offset: 0x0020E9E6
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x060049AA RID: 18858 RVA: 0x002107E9 File Offset: 0x0020E9E9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x060049AB RID: 18859 RVA: 0x002107F6 File Offset: 0x0020E9F6
		// (set) Token: 0x060049AC RID: 18860 RVA: 0x002107FE File Offset: 0x0020E9FE
		private bool AnyAttacksPlayedLastTurn
		{
			get
			{
				return this._anyAttacksPlayedLastTurn;
			}
			set
			{
				base.AssertMutable();
				this._anyAttacksPlayedLastTurn = value;
			}
		}

		// Token: 0x17000D12 RID: 3346
		// (get) Token: 0x060049AD RID: 18861 RVA: 0x0021080D File Offset: 0x0020EA0D
		// (set) Token: 0x060049AE RID: 18862 RVA: 0x00210815 File Offset: 0x0020EA15
		private bool AnyAttacksPlayedThisTurn
		{
			get
			{
				return this._anyAttacksPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._anyAttacksPlayedThisTurn = value;
			}
		}

		// Token: 0x17000D13 RID: 3347
		// (get) Token: 0x060049AF RID: 18863 RVA: 0x00210824 File Offset: 0x0020EA24
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x00210834 File Offset: 0x0020EA34
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (base.Owner != cardPlay.Card.Owner)
			{
				return Task.CompletedTask;
			}
			if (!CombatManager.Instance.IsInProgress)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			if (this.AnyAttacksPlayedLastTurn)
			{
				return Task.CompletedTask;
			}
			base.Status = RelicStatus.Normal;
			this.AnyAttacksPlayedThisTurn = true;
			return Task.CompletedTask;
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x002108A1 File Offset: 0x0020EAA1
		public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.AnyAttacksPlayedLastTurn = this.AnyAttacksPlayedThisTurn;
			this.AnyAttacksPlayedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x002108D4 File Offset: 0x0020EAD4
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner)
			{
				base.Status = RelicStatus.Active;
				if (base.Owner.Creature.CombatState.RoundNumber > 1)
				{
					if (!this.AnyAttacksPlayedLastTurn)
					{
						base.Flash();
						await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
					}
					this.AnyAttacksPlayedLastTurn = false;
					this.AnyAttacksPlayedThisTurn = false;
				}
			}
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x0021091F File Offset: 0x0020EB1F
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.AnyAttacksPlayedLastTurn = false;
			this.AnyAttacksPlayedThisTurn = false;
			return Task.CompletedTask;
		}

		// Token: 0x04002185 RID: 8581
		private bool _anyAttacksPlayedLastTurn;

		// Token: 0x04002186 RID: 8582
		private bool _anyAttacksPlayedThisTurn;
	}
}
