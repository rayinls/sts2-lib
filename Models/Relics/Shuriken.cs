using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200058E RID: 1422
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shuriken : RelicModel
	{
		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x0021AC64 File Offset: 0x00218E64
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06004F42 RID: 20290 RVA: 0x0021AC67 File Offset: 0x00218E67
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06004F43 RID: 20291 RVA: 0x0021AC73 File Offset: 0x00218E73
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.AttacksPlayedThisTurn % base.DynamicVars.Cards.IntValue;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06004F44 RID: 20292 RVA: 0x0021ACA5 File Offset: 0x00218EA5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new PowerVar<StrengthPower>(1m)
				});
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x0021ACC8 File Offset: 0x00218EC8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06004F46 RID: 20294 RVA: 0x0021ACD4 File Offset: 0x00218ED4
		// (set) Token: 0x06004F47 RID: 20295 RVA: 0x0021ACDC File Offset: 0x00218EDC
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06004F48 RID: 20296 RVA: 0x0021ACF1 File Offset: 0x00218EF1
		// (set) Token: 0x06004F49 RID: 20297 RVA: 0x0021ACF9 File Offset: 0x00218EF9
		private int AttacksPlayedThisTurn
		{
			get
			{
				return this._attacksPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._attacksPlayedThisTurn = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x0021AD10 File Offset: 0x00218F10
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars.Cards.IntValue;
				base.Status = ((this.AttacksPlayedThisTurn % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x0021AD5C File Offset: 0x00218F5C
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.AttacksPlayedThisTurn = 0;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x0021AD8C File Offset: 0x00218F8C
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					if (cardPlay.Card.Type == CardType.Attack)
					{
						int attacksPlayedThisTurn = this.AttacksPlayedThisTurn;
						this.AttacksPlayedThisTurn = attacksPlayedThisTurn + 1;
						int intValue = base.DynamicVars.Cards.IntValue;
						if (this.AttacksPlayedThisTurn % intValue == 0)
						{
							TaskHelper.RunSafely(this.DoActivateVisuals());
							await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
						}
					}
				}
			}
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x0021ADD8 File Offset: 0x00218FD8
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x0021AE1B File Offset: 0x0021901B
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.AttacksPlayedThisTurn = 0;
			this.IsActivating = false;
			return Task.CompletedTask;
		}

		// Token: 0x04002218 RID: 8728
		private bool _isActivating;

		// Token: 0x04002219 RID: 8729
		private int _attacksPlayedThisTurn;
	}
}
