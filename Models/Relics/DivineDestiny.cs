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
	// Token: 0x020004E6 RID: 1254
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DivineDestiny : RelicModel
	{
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06004AFD RID: 19197 RVA: 0x00212FFF File Offset: 0x002111FF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06004AFE RID: 19198 RVA: 0x00213002 File Offset: 0x00211202
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StarsVar(6));
			}
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00213010 File Offset: 0x00211210
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
				}
			}
		}
	}
}
