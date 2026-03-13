using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000578 RID: 1400
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RedSkull : RelicModel
	{
		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x00219CB7 File Offset: 0x00217EB7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x06004ECA RID: 20170 RVA: 0x00219CBA File Offset: 0x00217EBA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("HpThreshold", 50m),
					new PowerVar<StrengthPower>(3m)
				});
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x00219CE9 File Offset: 0x00217EE9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06004ECC RID: 20172 RVA: 0x00219CF5 File Offset: 0x00217EF5
		// (set) Token: 0x06004ECD RID: 20173 RVA: 0x00219CFD File Offset: 0x00217EFD
		private bool StrengthApplied
		{
			get
			{
				return this._strengthApplied;
			}
			set
			{
				base.AssertMutable();
				this._strengthApplied = value;
			}
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00219D0C File Offset: 0x00217F0C
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is CombatRoom)
			{
				await this.ModifyStrengthIfNecessary();
			}
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00219D57 File Offset: 0x00217F57
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.StrengthApplied = false;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00219D6C File Offset: 0x00217F6C
		public override async Task AfterCurrentHpChanged(Creature creature, decimal _)
		{
			if (CombatManager.Instance.IsInProgress)
			{
				await this.ModifyStrengthIfNecessary();
			}
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x00219DB0 File Offset: 0x00217FB0
		private async Task ModifyStrengthIfNecessary()
		{
			Creature creature = base.Owner.Creature;
			bool flag = creature.CurrentHp > creature.MaxHp * (base.DynamicVars["HpThreshold"].BaseValue / 100m);
			base.Status = (flag ? RelicStatus.Normal : RelicStatus.Active);
			decimal baseValue = base.DynamicVars.Strength.BaseValue;
			if (flag && this.StrengthApplied)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(creature, -baseValue, creature, null, false);
				this.StrengthApplied = false;
			}
			else if (!flag && !this.StrengthApplied)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(creature, baseValue, creature, null, false);
				this.StrengthApplied = true;
			}
		}

		// Token: 0x0400220E RID: 8718
		private const string _hpThresholdKey = "HpThreshold";

		// Token: 0x0400220F RID: 8719
		private bool _strengthApplied;
	}
}
