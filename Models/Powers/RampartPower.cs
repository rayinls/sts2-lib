using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200067C RID: 1660
	public sealed class RampartPower : PowerModel
	{
		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x060054AF RID: 21679 RVA: 0x00225037 File Offset: 0x00223237
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x060054B0 RID: 21680 RVA: 0x0022503A File Offset: 0x0022323A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x060054B1 RID: 21681 RVA: 0x0022503D File Offset: 0x0022323D
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00225040 File Offset: 0x00223240
		[NullableContext(1)]
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				IEnumerable<Creature> enumerable = base.CombatState.Enemies.Where((Creature c) => c.Monster is TurretOperator);
				foreach (Creature creature in enumerable)
				{
					await CreatureCmd.GainBlock(creature, base.Amount, ValueProp.Unpowered, null, false);
				}
				IEnumerator<Creature> enumerator = null;
			}
		}
	}
}
