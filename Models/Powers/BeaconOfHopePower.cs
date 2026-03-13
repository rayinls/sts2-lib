using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E0 RID: 1504
	public sealed class BeaconOfHopePower : PowerModel
	{
		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06005137 RID: 20791 RVA: 0x0021EF4F File Offset: 0x0021D14F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06005138 RID: 20792 RVA: 0x0021EF52 File Offset: 0x0021D152
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005139 RID: 20793 RVA: 0x0021EF58 File Offset: 0x0021D158
		[NullableContext(1)]
		public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, [Nullable(2)] CardModel cardSource)
		{
			if (!(amount < 1m))
			{
				if (creature == base.Owner)
				{
					if (base.CombatState.CurrentSide == base.Owner.Side)
					{
						IEnumerable<Creature> enumerable = from c in base.CombatState.GetTeammatesOf(base.Owner)
							where c != null && c.IsAlive && c.IsPlayer && c.Player.Creature != base.Owner
							select c;
						decimal amountToGive = amount * 0.5m * base.Amount;
						if (!(amountToGive < 1m))
						{
							foreach (Creature creature2 in enumerable)
							{
								await CreatureCmd.GainBlock(creature2, amountToGive, ValueProp.Unpowered, null, false);
							}
							IEnumerator<Creature> enumerator = null;
						}
					}
				}
			}
		}
	}
}
