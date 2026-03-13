using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000593 RID: 1427
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SneckoEye : RelicModel
	{
		// Token: 0x17000FD3 RID: 4051
		// (get) Token: 0x06004F6B RID: 20331 RVA: 0x0021B15F File Offset: 0x0021935F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06004F6C RID: 20332 RVA: 0x0021B162 File Offset: 0x00219362
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06004F6D RID: 20333 RVA: 0x0021B16F File Offset: 0x0021936F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<ConfusedPower>());
			}
		}

		// Token: 0x06004F6E RID: 20334 RVA: 0x0021B17C File Offset: 0x0021937C
		public override async Task AfterObtained()
		{
			if (CombatManager.Instance.IsInProgress)
			{
				await this.ApplyPower();
			}
		}

		// Token: 0x06004F6F RID: 20335 RVA: 0x0021B1C0 File Offset: 0x002193C0
		public override async Task BeforeCombatStart()
		{
			await this.ApplyPower();
		}

		// Token: 0x06004F70 RID: 20336 RVA: 0x0021B203 File Offset: 0x00219403
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.BaseValue;
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x0021B228 File Offset: 0x00219428
		private async Task ApplyPower()
		{
			await PowerCmd.Apply<ConfusedPower>(base.Owner.Creature, 1m, base.Owner.Creature, null, false);
			this.ApplyTestEnergyCostOverrideToPower();
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x0021B26B File Offset: 0x0021946B
		public void SetTestEnergyCostOverride(int value)
		{
			TestMode.AssertOn();
			base.AssertMutable();
			this._testEnergyCostOverride = value;
			this.ApplyTestEnergyCostOverrideToPower();
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x0021B288 File Offset: 0x00219488
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

		// Token: 0x0400221C RID: 8732
		private int _testEnergyCostOverride = -1;
	}
}
