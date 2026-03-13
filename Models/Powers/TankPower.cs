using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B9 RID: 1721
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class TankPower : PowerModel
	{
		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06005624 RID: 22052 RVA: 0x00227D72 File Offset: 0x00225F72
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06005625 RID: 22053 RVA: 0x00227D75 File Offset: 0x00225F75
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x00227D78 File Offset: 0x00225F78
		[return: Nullable(1)]
		public override async Task AfterApplied(Creature applier, CardModel cardSource)
		{
			foreach (Creature creature in base.CombatState.GetTeammatesOf(base.Owner))
			{
				if (creature.IsAlive && creature.IsPlayer && creature != base.Owner)
				{
					await PowerCmd.Apply<GuardedPower>(creature, base.Amount, base.Owner, null, false);
				}
			}
			IEnumerator<Creature> enumerator = null;
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x00227DBB File Offset: 0x00225FBB
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return 2m;
		}
	}
}
