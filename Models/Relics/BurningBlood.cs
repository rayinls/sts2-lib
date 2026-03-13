using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004CA RID: 1226
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BurningBlood : RelicModel
	{
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x00211EE7 File Offset: 0x002100E7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x00211EEA File Offset: 0x002100EA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(6m));
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x00211EFC File Offset: 0x002100FC
		public override async Task AfterCombatVictory(CombatRoom _)
		{
			if (!base.Owner.Creature.IsDead)
			{
				base.Flash();
				await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
			}
		}
	}
}
