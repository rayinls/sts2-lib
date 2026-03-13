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
	// Token: 0x02000500 RID: 1280
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ForgottenSoul : RelicModel
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06004B9A RID: 19354 RVA: 0x00213FF6 File Offset: 0x002121F6
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x00213FF9 File Offset: 0x002121F9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(1m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0021400C File Offset: 0x0021220C
		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool _)
		{
			if (card.Owner == base.Owner)
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
