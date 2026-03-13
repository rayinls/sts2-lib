using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F5 RID: 1269
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeHappyFlower : RelicModel
	{
		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06004B4E RID: 19278 RVA: 0x002138E3 File Offset: 0x00211AE3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06004B4F RID: 19279 RVA: 0x002138E6 File Offset: 0x00211AE6
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x002138E9 File Offset: 0x00211AE9
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

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06004B51 RID: 19281 RVA: 0x0021390F File Offset: 0x00211B0F
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x06004B52 RID: 19282 RVA: 0x00213913 File Offset: 0x00211B13
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new DynamicVar("Turns", 5m)
				});
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x06004B53 RID: 19283 RVA: 0x0021393C File Offset: 0x00211B3C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x06004B54 RID: 19284 RVA: 0x00213949 File Offset: 0x00211B49
		// (set) Token: 0x06004B55 RID: 19285 RVA: 0x00213951 File Offset: 0x00211B51
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

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06004B56 RID: 19286 RVA: 0x00213966 File Offset: 0x00211B66
		// (set) Token: 0x06004B57 RID: 19287 RVA: 0x0021396E File Offset: 0x00211B6E
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
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x00213984 File Offset: 0x00211B84
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				this.TurnsSeen = (this.TurnsSeen + 1) % base.DynamicVars["Turns"].IntValue;
				base.Status = ((this.TurnsSeen == base.DynamicVars["Turns"].IntValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
				if (this.TurnsSeen == 0)
				{
					TaskHelper.RunSafely(this.DoActivateVisuals());
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
			}
		}

		// Token: 0x06004B59 RID: 19289 RVA: 0x002139D0 File Offset: 0x00211BD0
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004B5A RID: 19290 RVA: 0x00213A13 File Offset: 0x00211C13
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x040021AB RID: 8619
		private const string _turnsKey = "Turns";

		// Token: 0x040021AC RID: 8620
		private bool _isActivating;

		// Token: 0x040021AD RID: 8621
		private int _turnsSeen;
	}
}
