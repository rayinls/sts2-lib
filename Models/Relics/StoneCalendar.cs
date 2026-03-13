using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000598 RID: 1432
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StoneCalendar : RelicModel
	{
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06004F8C RID: 20364 RVA: 0x0021B4C0 File Offset: 0x002196C0
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06004F8D RID: 20365 RVA: 0x0021B4C3 File Offset: 0x002196C3
		public override bool ShowCounter
		{
			get
			{
				return this.DisplayAmount > -1;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06004F8E RID: 20366 RVA: 0x0021B4D0 File Offset: 0x002196D0
		public override int DisplayAmount
		{
			get
			{
				if (!CombatManager.Instance.IsInProgress)
				{
					return -1;
				}
				if (base.IsCanonical)
				{
					return -1;
				}
				int intValue = base.DynamicVars["DamageTurn"].IntValue;
				if (this.IsActivating)
				{
					return intValue;
				}
				int roundNumber = base.Owner.Creature.CombatState.RoundNumber;
				if (roundNumber >= intValue)
				{
					return -1;
				}
				return roundNumber;
			}
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06004F8F RID: 20367 RVA: 0x0021B532 File Offset: 0x00219732
		// (set) Token: 0x06004F90 RID: 20368 RVA: 0x0021B53A File Offset: 0x0021973A
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
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06004F91 RID: 20369 RVA: 0x0021B54F File Offset: 0x0021974F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(52m, ValueProp.Unpowered),
					new DynamicVar("DamageTurn", 7m)
				});
			}
		}

		// Token: 0x06004F92 RID: 20370 RVA: 0x0021B580 File Offset: 0x00219780
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			if (combatState.RoundNumber == base.DynamicVars["DamageTurn"].IntValue)
			{
				base.Status = RelicStatus.Active;
			}
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x0021B5D8 File Offset: 0x002197D8
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Creature.Side)
			{
				int intValue = base.DynamicVars["DamageTurn"].IntValue;
				int roundNumber = base.Owner.Creature.CombatState.RoundNumber;
				base.Status = RelicStatus.Normal;
				if (roundNumber == intValue)
				{
					TaskHelper.RunSafely(this.DoActivateVisuals());
					await CreatureCmd.Damage(choiceContext, base.Owner.Creature.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature);
					base.InvokeDisplayAmountChanged();
				}
			}
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x0021B62B File Offset: 0x0021982B
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x0021B63F File Offset: 0x0021983F
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			base.Status = RelicStatus.Normal;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x0021B664 File Offset: 0x00219864
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x0400221D RID: 8733
		private const string _damageTurnKey = "DamageTurn";

		// Token: 0x0400221E RID: 8734
		private bool _isActivating;
	}
}
