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
	// Token: 0x02000522 RID: 1314
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Kunai : RelicModel
	{
		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x002159F3 File Offset: 0x00213BF3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x002159F6 File Offset: 0x00213BF6
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x00215A02 File Offset: 0x00213C02
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

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x00215A34 File Offset: 0x00213C34
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new PowerVar<DexterityPower>(1m)
				});
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x00215A57 File Offset: 0x00213C57
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x00215A63 File Offset: 0x00213C63
		// (set) Token: 0x06004C7B RID: 19579 RVA: 0x00215A6B File Offset: 0x00213C6B
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

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x00215A80 File Offset: 0x00213C80
		// (set) Token: 0x06004C7D RID: 19581 RVA: 0x00215A88 File Offset: 0x00213C88
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

		// Token: 0x06004C7E RID: 19582 RVA: 0x00215AA0 File Offset: 0x00213CA0
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

		// Token: 0x06004C7F RID: 19583 RVA: 0x00215AEC File Offset: 0x00213CEC
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

		// Token: 0x06004C80 RID: 19584 RVA: 0x00215B1C File Offset: 0x00213D1C
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
							await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null, false);
						}
					}
				}
			}
		}

		// Token: 0x06004C81 RID: 19585 RVA: 0x00215B68 File Offset: 0x00213D68
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004C82 RID: 19586 RVA: 0x00215BAB File Offset: 0x00213DAB
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.AttacksPlayedThisTurn = 0;
			this.IsActivating = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021CC RID: 8652
		private bool _isActivating;

		// Token: 0x040021CD RID: 8653
		private int _attacksPlayedThisTurn;
	}
}
