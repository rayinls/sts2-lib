using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004FA RID: 1274
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeSneckoEye : RelicModel
	{
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x00213BE5 File Offset: 0x00211DE5
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06004B75 RID: 19317 RVA: 0x00213BE8 File Offset: 0x00211DE8
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06004B76 RID: 19318 RVA: 0x00213BEC File Offset: 0x00211DEC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<ConfusedPower>());
			}
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00213BF8 File Offset: 0x00211DF8
		public override async Task AfterObtained()
		{
			if (CombatManager.Instance.IsInProgress)
			{
				await this.ApplyPower();
			}
		}

		// Token: 0x06004B78 RID: 19320 RVA: 0x00213C3C File Offset: 0x00211E3C
		public override async Task BeforeCombatStart()
		{
			await this.ApplyPower();
		}

		// Token: 0x06004B79 RID: 19321 RVA: 0x00213C80 File Offset: 0x00211E80
		private async Task ApplyPower()
		{
			await PowerCmd.Apply<ConfusedPower>(base.Owner.Creature, 1m, base.Owner.Creature, null, false);
			this.ApplyTestEnergyCostOverrideToPower();
		}

		// Token: 0x06004B7A RID: 19322 RVA: 0x00213CC3 File Offset: 0x00211EC3
		public void SetTestEnergyCostOverride(int value)
		{
			TestMode.AssertOn();
			base.AssertMutable();
			this._testEnergyCostOverride = value;
			this.ApplyTestEnergyCostOverrideToPower();
		}

		// Token: 0x06004B7B RID: 19323 RVA: 0x00213CE0 File Offset: 0x00211EE0
		private void ApplyTestEnergyCostOverrideToPower()
		{
			if (this._testEnergyCostOverride < 0)
			{
				return;
			}
			ConfusedPower power = base.Owner.Creature.GetPower<ConfusedPower>();
			if (power == null)
			{
				return;
			}
			power.TestEnergyCostOverride = this._testEnergyCostOverride;
		}

		// Token: 0x040021AF RID: 8623
		private int _testEnergyCostOverride = -1;
	}
}
