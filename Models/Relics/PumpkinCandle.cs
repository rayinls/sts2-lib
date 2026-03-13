using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000572 RID: 1394
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PumpkinCandle : RelicModel
	{
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06004E9D RID: 20125 RVA: 0x0021986B File Offset: 0x00217A6B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06004E9E RID: 20126 RVA: 0x0021986E File Offset: 0x00217A6E
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x00219871 File Offset: 0x00217A71
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x0021987E File Offset: 0x00217A7E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06004EA1 RID: 20129 RVA: 0x0021988B File Offset: 0x00217A8B
		// (set) Token: 0x06004EA2 RID: 20130 RVA: 0x00219893 File Offset: 0x00217A93
		[SavedProperty]
		public int ActiveAct
		{
			get
			{
				return this._activeAct;
			}
			set
			{
				base.AssertMutable();
				this._activeAct = value;
			}
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x002198A2 File Offset: 0x00217AA2
		public override Task AfterObtained()
		{
			this.ActiveAct = base.Owner.RunState.CurrentActIndex;
			return Task.CompletedTask;
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x002198C0 File Offset: 0x00217AC0
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			if (this.ActiveAct != base.Owner.RunState.CurrentActIndex)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.IntValue;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x0021990D File Offset: 0x00217B0D
		public override Task AfterRoomEntered(AbstractRoom _)
		{
			base.Status = ((this.ActiveAct == base.Owner.RunState.CurrentActIndex) ? RelicStatus.Normal : RelicStatus.Disabled);
			return Task.CompletedTask;
		}

		// Token: 0x04002208 RID: 8712
		private int _activeAct = -1;
	}
}
