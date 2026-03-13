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
	// Token: 0x02000514 RID: 1300
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HappyFlower : RelicModel
	{
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x06004C18 RID: 19480 RVA: 0x00214F94 File Offset: 0x00213194
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x06004C19 RID: 19481 RVA: 0x00214F97 File Offset: 0x00213197
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06004C1A RID: 19482 RVA: 0x00214F9A File Offset: 0x0021319A
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

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06004C1B RID: 19483 RVA: 0x00214FC0 File Offset: 0x002131C0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new DynamicVar("Turns", 3m)
				});
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06004C1C RID: 19484 RVA: 0x00214FE9 File Offset: 0x002131E9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06004C1D RID: 19485 RVA: 0x00214FF6 File Offset: 0x002131F6
		// (set) Token: 0x06004C1E RID: 19486 RVA: 0x00214FFE File Offset: 0x002131FE
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

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06004C1F RID: 19487 RVA: 0x00215013 File Offset: 0x00213213
		// (set) Token: 0x06004C20 RID: 19488 RVA: 0x0021501B File Offset: 0x0021321B
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

		// Token: 0x06004C21 RID: 19489 RVA: 0x00215030 File Offset: 0x00213230
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

		// Token: 0x06004C22 RID: 19490 RVA: 0x0021507C File Offset: 0x0021327C
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x002150BF File Offset: 0x002132BF
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x040021BF RID: 8639
		public const int turnsThreshold = 3;

		// Token: 0x040021C0 RID: 8640
		private const string _turnsKey = "Turns";

		// Token: 0x040021C1 RID: 8641
		private bool _isActivating;

		// Token: 0x040021C2 RID: 8642
		private int _turnsSeen;
	}
}
