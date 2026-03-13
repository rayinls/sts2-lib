using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000523 RID: 1315
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Kusarigama : RelicModel
	{
		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x00215BCF File Offset: 0x00213DCF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06004C85 RID: 19589 RVA: 0x00215BD2 File Offset: 0x00213DD2
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06004C86 RID: 19590 RVA: 0x00215BDE File Offset: 0x00213DDE
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

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06004C87 RID: 19591 RVA: 0x00215C10 File Offset: 0x00213E10
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new DamageVar(6m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06004C88 RID: 19592 RVA: 0x00215C35 File Offset: 0x00213E35
		// (set) Token: 0x06004C89 RID: 19593 RVA: 0x00215C3D File Offset: 0x00213E3D
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

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06004C8A RID: 19594 RVA: 0x00215C52 File Offset: 0x00213E52
		// (set) Token: 0x06004C8B RID: 19595 RVA: 0x00215C5A File Offset: 0x00213E5A
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

		// Token: 0x06004C8C RID: 19596 RVA: 0x00215C70 File Offset: 0x00213E70
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

		// Token: 0x06004C8D RID: 19597 RVA: 0x00215CBC File Offset: 0x00213EBC
		public override Task BeforeCombatStart()
		{
			this.AttacksPlayedThisTurn = 0;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x00215CD1 File Offset: 0x00213ED1
		public override Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			this.AttacksPlayedThisTurn = 0;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x00215CE8 File Offset: 0x00213EE8
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
							Creature creature = base.Owner.RunState.Rng.CombatTargets.NextItem<Creature>(base.Owner.Creature.CombatState.HittableEnemies);
							if (creature != null)
							{
								TaskHelper.RunSafely(this.DoActivateVisuals());
								await CreatureCmd.Damage(context, creature, base.DynamicVars.Damage, base.Owner.Creature);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x00215D3C File Offset: 0x00213F3C
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x00215D7F File Offset: 0x00213F7F
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.IsActivating = false;
			return Task.CompletedTask;
		}

		// Token: 0x040021CE RID: 8654
		private bool _isActivating;

		// Token: 0x040021CF RID: 8655
		private int _attacksPlayedThisTurn;
	}
}
