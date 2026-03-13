using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A8 RID: 1704
	public sealed class StarNextTurnPower : PowerModel
	{
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x060055B7 RID: 21943 RVA: 0x00226F47 File Offset: 0x00225147
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x060055B8 RID: 21944 RVA: 0x00226F4A File Offset: 0x0022514A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055B9 RID: 21945 RVA: 0x00226F50 File Offset: 0x00225150
		[NullableContext(1)]
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner.Player)
			{
				await PlayerCmd.GainStars(base.Amount, player);
				await PowerCmd.Remove(this);
			}
		}
	}
}
