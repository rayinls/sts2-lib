using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000627 RID: 1575
	public sealed class ForbiddenGrimoirePower : PowerModel
	{
		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x00221964 File Offset: 0x0021FB64
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x060052C4 RID: 21188 RVA: 0x00221967 File Offset: 0x0021FB67
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052C5 RID: 21189 RVA: 0x0022196C File Offset: 0x0021FB6C
		[NullableContext(1)]
		public override Task AfterCombatEnd(CombatRoom room)
		{
			for (int i = 0; i < base.Amount; i++)
			{
				room.AddExtraReward(base.Owner.Player, new CardRemovalReward(base.Owner.Player));
			}
			return Task.CompletedTask;
		}
	}
}
