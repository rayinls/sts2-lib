using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200055F RID: 1375
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ParryingShield : RelicModel
	{
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06004E22 RID: 20002 RVA: 0x00218A53 File Offset: 0x00216C53
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06004E23 RID: 20003 RVA: 0x00218A56 File Offset: 0x00216C56
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new BlockVar(10m, ValueProp.Unpowered),
					new DamageVar(6m, ValueProp.Unpowered)
				});
			}
		}

		// Token: 0x06004E24 RID: 20004 RVA: 0x00218A84 File Offset: 0x00216C84
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				if (!(base.Owner.Creature.Block < base.DynamicVars.Block.BaseValue))
				{
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
