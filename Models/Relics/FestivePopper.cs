using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004FE RID: 1278
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FestivePopper : RelicModel
	{
		// Token: 0x17000E00 RID: 3584
		// (get) Token: 0x06004B90 RID: 19344 RVA: 0x00213F03 File Offset: 0x00212103
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x00213F06 File Offset: 0x00212106
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(9m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x00213F1C File Offset: 0x0021211C
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				CombatState combatState = player.Creature.CombatState;
				if (combatState.RoundNumber == 1)
				{
					base.Flash();
					VfxCmd.PlayOnCreatureCenters(combatState.HittableEnemies, "vfx/vfx_attack_slash");
					await CreatureCmd.Damage(choiceContext, combatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature);
				}
			}
		}
	}
}
