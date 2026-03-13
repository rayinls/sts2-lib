using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000658 RID: 1624
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MindRotPower : PowerModel
	{
		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060053D3 RID: 21459 RVA: 0x002238B3 File Offset: 0x00221AB3
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060053D4 RID: 21460 RVA: 0x002238B6 File Offset: 0x00221AB6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053D5 RID: 21461 RVA: 0x002238B9 File Offset: 0x00221AB9
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner.Player)
			{
				return count;
			}
			return Math.Max(0m, count - base.Amount);
		}

		// Token: 0x060053D6 RID: 21462 RVA: 0x002238E6 File Offset: 0x00221AE6
		public override Task AfterModifyingHandDraw()
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
