using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200052D RID: 1325
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LizardTail : RelicModel
	{
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x00216598 File Offset: 0x00214798
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x0021659B File Offset: 0x0021479B
		public override bool IsUsedUp
		{
			get
			{
				return this._wasUsed;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x002165A3 File Offset: 0x002147A3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(50m));
			}
		}

		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x002165B6 File Offset: 0x002147B6
		// (set) Token: 0x06004CD8 RID: 19672 RVA: 0x002165BE File Offset: 0x002147BE
		[SavedProperty]
		public bool WasUsed
		{
			get
			{
				return this._wasUsed;
			}
			set
			{
				base.AssertMutable();
				this._wasUsed = value;
				if (this.IsUsedUp)
				{
					base.Status = RelicStatus.Disabled;
				}
			}
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x002165DC File Offset: 0x002147DC
		public override bool ShouldDieLate(Creature creature)
		{
			return creature != base.Owner.Creature || this.WasUsed;
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x002165FC File Offset: 0x002147FC
		public override async Task AfterPreventingDeath(Creature creature)
		{
			base.Flash();
			this.WasUsed = true;
			await CreatureCmd.Heal(creature, creature.MaxHp * (base.DynamicVars.Heal.BaseValue / 100m), true);
		}

		// Token: 0x040021D8 RID: 8664
		private bool _wasUsed;
	}
}
