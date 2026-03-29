using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RedCombustPower : PowerModel
	{
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new DamageVar("SelfDamage", 1m, ValueProp.Unblockable | ValueProp.Unpowered) };
			}
		}

		public override async Task BeforeTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await CreatureCmd.Damage(choiceContext, base.Owner, ((DamageVar)base.DynamicVars["SelfDamage"]).BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
				await CreatureCmd.Damage(choiceContext, base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
			}
		}
	}
}
