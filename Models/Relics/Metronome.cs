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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200053C RID: 1340
	[NullableContext(1)]
	[Nullable(0)]
	public class Metronome : RelicModel
	{
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06004D21 RID: 19745 RVA: 0x00216D77 File Offset: 0x00214F77
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06004D22 RID: 19746 RVA: 0x00216D7A File Offset: 0x00214F7A
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress && (this.OrbsChanneled < base.DynamicVars["OrbCount"].IntValue || this.IsActivating);
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x00216DAF File Offset: 0x00214FAF
		public override int DisplayAmount
		{
			get
			{
				return Math.Min(this.OrbsChanneled, base.DynamicVars["OrbCount"].IntValue);
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x00216DD1 File Offset: 0x00214FD1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(30m, ValueProp.Unpowered),
					new DynamicVar("OrbCount", 7m)
				});
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06004D25 RID: 19749 RVA: 0x00216E01 File Offset: 0x00215001
		// (set) Token: 0x06004D26 RID: 19750 RVA: 0x00216E09 File Offset: 0x00215009
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

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06004D27 RID: 19751 RVA: 0x00216E1E File Offset: 0x0021501E
		// (set) Token: 0x06004D28 RID: 19752 RVA: 0x00216E26 File Offset: 0x00215026
		private int OrbsChanneled
		{
			get
			{
				return this._orbsChanneled;
			}
			set
			{
				base.AssertMutable();
				this._orbsChanneled = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x00216E3B File Offset: 0x0021503B
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			this.OrbsChanneled = 0;
			this.UpdateDisplay();
			return Task.CompletedTask;
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x00216E60 File Offset: 0x00215060
		public override async Task AfterOrbChanneled(PlayerChoiceContext choiceContext, Player player, OrbModel orb)
		{
			if (player == base.Owner)
			{
				int orbsChanneled = this.OrbsChanneled;
				this.OrbsChanneled = orbsChanneled + 1;
				if (this.OrbsChanneled == base.DynamicVars["OrbCount"].IntValue)
				{
					TaskHelper.RunSafely(this.DoActivateVisuals());
					await CreatureCmd.Damage(choiceContext, base.Owner.Creature.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature);
				}
			}
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00216EB3 File Offset: 0x002150B3
		public override Task AfterCombatEnd(CombatRoom _)
		{
			base.Status = RelicStatus.Normal;
			this.OrbsChanneled = 0;
			this.UpdateDisplay();
			return Task.CompletedTask;
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x00216ED0 File Offset: 0x002150D0
		private void UpdateDisplay()
		{
			int intValue = base.DynamicVars["OrbCount"].IntValue;
			if (this.OrbsChanneled == intValue - 1 && !this.IsActivating)
			{
				base.Status = RelicStatus.Active;
			}
			else
			{
				base.Status = RelicStatus.Normal;
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x00216F1C File Offset: 0x0021511C
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021DC RID: 8668
		private const string _orbCountKey = "OrbCount";

		// Token: 0x040021DD RID: 8669
		private bool _isActivating;

		// Token: 0x040021DE RID: 8670
		private int _orbsChanneled;
	}
}
