using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A8 RID: 1448
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Tingsha : RelicModel
	{
		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x06004FEA RID: 20458 RVA: 0x0021BF3C File Offset: 0x0021A13C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06004FEB RID: 20459 RVA: 0x0021BF3F File Offset: 0x0021A13F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(3m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x0021BF54 File Offset: 0x0021A154
		public override async Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
		{
			if (card.Owner == base.Owner)
			{
				if (base.Owner.Creature.Side == base.Owner.Creature.CombatState.CurrentSide)
				{
					base.Flash();
					Creature creature = base.Owner.RunState.Rng.CombatTargets.NextItem<Creature>(base.Owner.Creature.CombatState.HittableEnemies);
					if (creature != null)
					{
						VfxCmd.PlayOnCreatureCenter(creature, "vfx/vfx_attack_blunt");
						await CreatureCmd.Damage(choiceContext, creature, base.DynamicVars.Damage, base.Owner.Creature);
					}
				}
			}
		}
	}
}
