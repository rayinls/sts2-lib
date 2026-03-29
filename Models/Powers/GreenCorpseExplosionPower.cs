using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GreenCorpseExplosionPower : PowerModel
	{
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature creature, bool wasRemovalPrevented, float deathAnimLength)
		{
			if (wasRemovalPrevented)
			{
				return;
			}
			if (creature != base.Owner)
			{
				return;
			}
			decimal damage = base.Owner.MaxHp;
			List<Creature> list = base.CombatState.GetCreaturesOnSide(base.Owner.Side).Where((Creature c) => c != base.Owner && c.IsAlive).ToList<Creature>();
			if (list.Count == 0)
			{
				return;
			}
			await CreatureCmd.Damage(choiceContext, list, damage, ValueProp.Unpowered, base.Applier ?? base.Owner, null);
		}
	}
}
