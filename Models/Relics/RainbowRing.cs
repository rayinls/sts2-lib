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
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000575 RID: 1397
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RainbowRing : RelicModel
	{
		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x00219A57 File Offset: 0x00217C57
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x00219A5A File Offset: 0x00217C5A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x00219A77 File Offset: 0x00217C77
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(1m),
					new PowerVar<DexterityPower>(1m)
				});
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x00219A9E File Offset: 0x00217C9E
		// (set) Token: 0x06004EB6 RID: 20150 RVA: 0x00219AA6 File Offset: 0x00217CA6
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
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x06004EB7 RID: 20151 RVA: 0x00219AB5 File Offset: 0x00217CB5
		// (set) Token: 0x06004EB8 RID: 20152 RVA: 0x00219ABD File Offset: 0x00217CBD
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
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x00219ACC File Offset: 0x00217CCC
		// (set) Token: 0x06004EBA RID: 20154 RVA: 0x00219AD4 File Offset: 0x00217CD4
		private int PowersPlayedThisTurn
		{
			get
			{
				return this._powersPlayedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._powersPlayedThisTurn = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x00219AE3 File Offset: 0x00217CE3
		// (set) Token: 0x06004EBC RID: 20156 RVA: 0x00219AEB File Offset: 0x00217CEB
		private int ActivationCountThisTurn
		{
			get
			{
				return this._activationCountThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._activationCountThisTurn = value;
				base.Status = ((this._activationCountThisTurn > 0) ? RelicStatus.Active : RelicStatus.Normal);
			}
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x00219B0D File Offset: 0x00217D0D
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this.AttacksPlayedThisTurn = 0;
			this.SkillsPlayedThisTurn = 0;
			this.PowersPlayedThisTurn = 0;
			this.ActivationCountThisTurn = 0;
			return Task.CompletedTask;
		}

		// Token: 0x06004EBE RID: 20158 RVA: 0x00219B4C File Offset: 0x00217D4C
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (CombatManager.Instance.IsInProgress)
				{
					if (this.ActivationCountThisTurn < 1)
					{
						this.AttacksPlayedThisTurn += ((cardPlay.Card.Type == CardType.Attack) ? 1 : 0);
						this.SkillsPlayedThisTurn += ((cardPlay.Card.Type == CardType.Skill) ? 1 : 0);
						this.PowersPlayedThisTurn += ((cardPlay.Card.Type == CardType.Power) ? 1 : 0);
						if (this.AttacksPlayedThisTurn > 0 && this.SkillsPlayedThisTurn > 0 && this.PowersPlayedThisTurn > 0)
						{
							base.Flash();
							await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
							await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null, false);
							this.ActivationCountThisTurn++;
						}
					}
				}
			}
		}

		// Token: 0x06004EBF RID: 20159 RVA: 0x00219B97 File Offset: 0x00217D97
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.AttacksPlayedThisTurn = 0;
			this.SkillsPlayedThisTurn = 0;
			this.PowersPlayedThisTurn = 0;
			this.ActivationCountThisTurn = 0;
			return Task.CompletedTask;
		}

		// Token: 0x0400220A RID: 8714
		private int _attacksPlayedThisTurn;

		// Token: 0x0400220B RID: 8715
		private int _skillsPlayedThisTurn;

		// Token: 0x0400220C RID: 8716
		private int _powersPlayedThisTurn;

		// Token: 0x0400220D RID: 8717
		private int _activationCountThisTurn;
	}
}
