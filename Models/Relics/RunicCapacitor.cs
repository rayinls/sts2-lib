using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000583 RID: 1411
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RunicCapacitor : RelicModel
	{
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06004F0A RID: 20234 RVA: 0x0021A3FC File Offset: 0x002185FC
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06004F0B RID: 20235 RVA: 0x0021A3FF File Offset: 0x002185FF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(3));
			}
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0021A40C File Offset: 0x0021860C
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					await OrbCmd.AddSlots(base.Owner, base.DynamicVars.Repeat.IntValue);
				}
			}
		}
	}
}
