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
	// Token: 0x020006D5 RID: 1749
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WraithFormPower : PowerModel
	{
		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x060056E1 RID: 22241 RVA: 0x0022946E File Offset: 0x0022766E
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x060056E2 RID: 22242 RVA: 0x00229471 File Offset: 0x00227671
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x00229474 File Offset: 0x00227674
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DexterityPower>());
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x00229480 File Offset: 0x00227680
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Apply<DexterityPower>(base.Owner, -base.Amount, base.Owner, null, false);
			}
		}
	}
}
