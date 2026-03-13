using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004EF RID: 1263
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EmberTea : RelicModel
	{
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x002134D7 File Offset: 0x002116D7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06004B2A RID: 19242 RVA: 0x002134DA File Offset: 0x002116DA
		public override bool IsUsedUp
		{
			get
			{
				return this.CombatsLeft <= 0;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06004B2B RID: 19243 RVA: 0x002134E8 File Offset: 0x002116E8
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06004B2C RID: 19244 RVA: 0x002134EB File Offset: 0x002116EB
		public override int DisplayAmount
		{
			get
			{
				return Math.Max(0, this.CombatsLeft);
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06004B2D RID: 19245 RVA: 0x002134F9 File Offset: 0x002116F9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Combats", this.CombatsLeft),
					new PowerVar<StrengthPower>(2m)
				});
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06004B2E RID: 19246 RVA: 0x0021352C File Offset: 0x0021172C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06004B2F RID: 19247 RVA: 0x00213538 File Offset: 0x00211738
		// (set) Token: 0x06004B30 RID: 19248 RVA: 0x00213540 File Offset: 0x00211740
		[SavedProperty]
		public int CombatsLeft
		{
			get
			{
				return this._combatsLeft;
			}
			set
			{
				base.AssertMutable();
				this._combatsLeft = value;
				base.DynamicVars["Combats"].BaseValue = this._combatsLeft;
				base.InvokeDisplayAmountChanged();
				if (this.IsUsedUp)
				{
					base.Status = RelicStatus.Disabled;
				}
			}
		}

		// Token: 0x06004B31 RID: 19249 RVA: 0x00213590 File Offset: 0x00211790
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (!this.IsUsedUp)
			{
				if (room is CombatRoom)
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, null, null, false);
					this.CombatsLeft--;
				}
			}
		}

		// Token: 0x040021A9 RID: 8617
		private const string _combatsKey = "Combats";

		// Token: 0x040021AA RID: 8618
		private int _combatsLeft = 5;
	}
}
