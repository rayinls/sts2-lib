using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200064C RID: 1612
	public sealed class JuggernautPower : PowerModel
	{
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x06005398 RID: 21400 RVA: 0x00223251 File Offset: 0x00221451
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x00223254 File Offset: 0x00221454
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600539A RID: 21402 RVA: 0x00223258 File Offset: 0x00221458
		[NullableContext(1)]
		public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, [Nullable(2)] CardModel cardSource)
		{
			if (!(amount <= 0m))
			{
				if (creature == base.Owner)
				{
					IReadOnlyList<Creature> hittableEnemies = base.CombatState.HittableEnemies;
					if (hittableEnemies.Count != 0)
					{
						Creature creature2 = base.Owner.Player.RunState.Rng.CombatTargets.NextItem<Creature>(hittableEnemies);
						base.Flash();
						await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), creature2, base.Amount, ValueProp.Unpowered, base.Owner, null);
					}
				}
			}
		}
	}
}
