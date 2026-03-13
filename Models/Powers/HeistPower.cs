using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200063C RID: 1596
	public sealed class HeistPower : PowerModel
	{
		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06005339 RID: 21305 RVA: 0x0022268B File Offset: 0x0022088B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x0600533A RID: 21306 RVA: 0x0022268E File Offset: 0x0022088E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170011A0 RID: 4512
		// (get) Token: 0x0600533B RID: 21307 RVA: 0x00222691 File Offset: 0x00220891
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00222694 File Offset: 0x00220894
		[NullableContext(1)]
		public override Task BeforeDeath(Creature target)
		{
			if (base.Owner != target)
			{
				return Task.CompletedTask;
			}
			CombatRoom combatRoom = base.CombatState.RunState.CurrentRoom as CombatRoom;
			if (combatRoom != null)
			{
				combatRoom.AddExtraReward(base.Target.Player, new GoldReward(base.Amount, base.Target.Player, true));
			}
			return Task.CompletedTask;
		}
	}
}
