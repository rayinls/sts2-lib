using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000630 RID: 1584
	public sealed class GenesisPower : PowerModel
	{
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x060052F2 RID: 21234 RVA: 0x00221F07 File Offset: 0x00220107
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x060052F3 RID: 21235 RVA: 0x00221F0A File Offset: 0x0022010A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052F4 RID: 21236 RVA: 0x00221F10 File Offset: 0x00220110
		[NullableContext(1)]
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner.Player)
			{
				base.Flash();
				await PlayerCmd.GainStars(base.Amount, base.Owner.Player);
			}
		}
	}
}
