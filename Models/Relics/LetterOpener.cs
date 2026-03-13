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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200052C RID: 1324
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LetterOpener : RelicModel
	{
		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004CC6 RID: 19654 RVA: 0x002163C7 File Offset: 0x002145C7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06004CC7 RID: 19655 RVA: 0x002163CA File Offset: 0x002145CA
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06004CC8 RID: 19656 RVA: 0x002163D6 File Offset: 0x002145D6
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.SkillsPlayedThisTurn % base.DynamicVars.Cards.IntValue;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06004CC9 RID: 19657 RVA: 0x00216408 File Offset: 0x00214608
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new DamageVar(5m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06004CCA RID: 19658 RVA: 0x0021642D File Offset: 0x0021462D
		// (set) Token: 0x06004CCB RID: 19659 RVA: 0x00216435 File Offset: 0x00214635
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

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x06004CCC RID: 19660 RVA: 0x0021644A File Offset: 0x0021464A
		// (set) Token: 0x06004CCD RID: 19661 RVA: 0x00216452 File Offset: 0x00214652
		private int SkillsPlayedThisTurn
		{
			get
			{
				return this._skillsPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._skillsPlayedThisTurn = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004CCE RID: 19662 RVA: 0x00216468 File Offset: 0x00214668
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars.Cards.IntValue;
				base.Status = ((this.SkillsPlayedThisTurn % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004CCF RID: 19663 RVA: 0x002164B4 File Offset: 0x002146B4
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.SkillsPlayedThisTurn = 0;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004CD0 RID: 19664 RVA: 0x002164E4 File Offset: 0x002146E4
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					if (cardPlay.Card.Type == CardType.Skill)
					{
						int skillsPlayedThisTurn = this.SkillsPlayedThisTurn;
						this.SkillsPlayedThisTurn = skillsPlayedThisTurn + 1;
						int intValue = base.DynamicVars.Cards.IntValue;
						if (this.SkillsPlayedThisTurn % intValue == 0)
						{
							TaskHelper.RunSafely(this.DoActivateVisuals());
							await CreatureCmd.Damage(context, base.Owner.Creature.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature);
						}
					}
				}
			}
		}

		// Token: 0x06004CD1 RID: 19665 RVA: 0x00216538 File Offset: 0x00214738
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x0021657B File Offset: 0x0021477B
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.IsActivating = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021D6 RID: 8662
		private bool _isActivating;

		// Token: 0x040021D7 RID: 8663
		private int _skillsPlayedThisTurn;
	}
}
