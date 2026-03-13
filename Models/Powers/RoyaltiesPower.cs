using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000688 RID: 1672
	public sealed class RoyaltiesPower : PowerModel
	{
		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x060054FE RID: 21758 RVA: 0x00225A7A File Offset: 0x00223C7A
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x060054FF RID: 21759 RVA: 0x00225A7D File Offset: 0x00223C7D
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00225A80 File Offset: 0x00223C80
		[NullableContext(1)]
		public override Task AfterCombatEnd(CombatRoom room)
		{
			room.AddExtraReward(base.Owner.Player, new GoldReward(base.Amount, base.Owner.Player, false));
			return Task.CompletedTask;
		}
	}
}
