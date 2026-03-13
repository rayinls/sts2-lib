using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000539 RID: 1337
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MeatOnTheBone : RelicModel
	{
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06004D12 RID: 19730 RVA: 0x00216B63 File Offset: 0x00214D63
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x00216B66 File Offset: 0x00214D66
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("HpThreshold", 50m),
					new HealVar(12m)
				});
			}
		}

		// Token: 0x06004D14 RID: 19732 RVA: 0x00216B96 File Offset: 0x00214D96
		public override Task BeforeCombatStart()
		{
			if (this.WillHealOnCombatFinished())
			{
				base.Status = RelicStatus.Active;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x00216BAC File Offset: 0x00214DAC
		public override Task AfterCurrentHpChanged(Creature creature, decimal delta)
		{
			if (creature != base.Owner.Creature)
			{
				return Task.CompletedTask;
			}
			if (!CombatManager.Instance.IsInProgress)
			{
				return Task.CompletedTask;
			}
			base.Status = (this.WillHealOnCombatFinished() ? RelicStatus.Active : RelicStatus.Normal);
			return Task.CompletedTask;
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x00216BEC File Offset: 0x00214DEC
		public override async Task AfterCombatVictoryEarly(CombatRoom _)
		{
			if (!base.Owner.Creature.IsDead)
			{
				Creature creature = base.Owner.Creature;
				if (this.WillHealOnCombatFinished())
				{
					base.Status = RelicStatus.Normal;
					await CreatureCmd.Heal(creature, base.DynamicVars.Heal.BaseValue, true);
				}
			}
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x00216C30 File Offset: 0x00214E30
		private bool WillHealOnCombatFinished()
		{
			Creature creature = base.Owner.Creature;
			int num = (int)(creature.MaxHp * (base.DynamicVars["HpThreshold"].BaseValue / 100m));
			return creature.CurrentHp <= num;
		}

		// Token: 0x040021DA RID: 8666
		private const string _hpThresholdKey = "HpThreshold";
	}
}
