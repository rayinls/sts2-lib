using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005ED RID: 1517
	public sealed class ChildOfTheStarsPower : PowerModel
	{
		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06005184 RID: 20868 RVA: 0x0021F82E File Offset: 0x0021DA2E
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06005185 RID: 20869 RVA: 0x0021F831 File Offset: 0x0021DA31
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x0021F834 File Offset: 0x0021DA34
		[NullableContext(1)]
		public override async Task AfterStarsSpent(int amount, Player spender)
		{
			if (amount > 0)
			{
				if (spender == base.Owner.Player)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner, base.Amount * amount, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}
