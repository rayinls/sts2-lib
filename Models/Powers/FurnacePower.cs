using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200062E RID: 1582
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FurnacePower : PowerModel
	{
		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x060052E5 RID: 21221 RVA: 0x00221D85 File Offset: 0x0021FF85
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060052E6 RID: 21222 RVA: 0x00221D88 File Offset: 0x0021FF88
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060052E7 RID: 21223 RVA: 0x00221D8B File Offset: 0x0021FF8B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromForge();
			}
		}

		// Token: 0x060052E8 RID: 21224 RVA: 0x00221D94 File Offset: 0x0021FF94
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				await ForgeCmd.Forge(base.Amount, base.Owner.Player, this);
			}
		}
	}
}
