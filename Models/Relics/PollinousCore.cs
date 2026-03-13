using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000569 RID: 1385
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PollinousCore : RelicModel
	{
		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x06004E69 RID: 20073 RVA: 0x00219301 File Offset: 0x00217501
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000F56 RID: 3926
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x00219304 File Offset: 0x00217504
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F57 RID: 3927
		// (get) Token: 0x06004E6B RID: 20075 RVA: 0x00219307 File Offset: 0x00217507
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.TurnsSeen;
				}
				return base.DynamicVars["Turns"].IntValue;
			}
		}

		// Token: 0x17000F58 RID: 3928
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x0021932D File Offset: 0x0021752D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(2),
					new DynamicVar("Turns", 4m)
				});
			}
		}

		// Token: 0x17000F59 RID: 3929
		// (get) Token: 0x06004E6D RID: 20077 RVA: 0x00219356 File Offset: 0x00217556
		// (set) Token: 0x06004E6E RID: 20078 RVA: 0x0021935E File Offset: 0x0021755E
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

		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06004E6F RID: 20079 RVA: 0x00219373 File Offset: 0x00217573
		// (set) Token: 0x06004E70 RID: 20080 RVA: 0x0021937B File Offset: 0x0021757B
		[SavedProperty]
		public int TurnsSeen
		{
			get
			{
				return this._turnsSeen;
			}
			set
			{
				base.AssertMutable();
				this._turnsSeen = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x00219390 File Offset: 0x00217590
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars["Turns"].IntValue;
				base.Status = ((this.TurnsSeen == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x002193E0 File Offset: 0x002175E0
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			int turnsSeen = this.TurnsSeen;
			this.TurnsSeen = turnsSeen + 1;
			return Task.CompletedTask;
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0021941B File Offset: 0x0021761B
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x0021942C File Offset: 0x0021762C
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			if (this.TurnsSeen != base.DynamicVars["Turns"].IntValue)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.BaseValue;
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x00219479 File Offset: 0x00217679
		public override Task AfterModifyingHandDraw()
		{
			this.TurnsSeen = 0;
			TaskHelper.RunSafely(this.DoActivateVisuals());
			return Task.CompletedTask;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x00219494 File Offset: 0x00217694
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x04002204 RID: 8708
		private const string _turnsKey = "Turns";

		// Token: 0x04002205 RID: 8709
		private bool _isActivating;

		// Token: 0x04002206 RID: 8710
		private int _turnsSeen;
	}
}
