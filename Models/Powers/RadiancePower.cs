using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200067A RID: 1658
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RadiancePower : PowerModel
	{
		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x00224F09 File Offset: 0x00223109
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x00224F0C File Offset: 0x0022310C
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x00224F0F File Offset: 0x0022310F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x060054A6 RID: 21670 RVA: 0x00224F1C File Offset: 0x0022311C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x00224F2C File Offset: 0x0022312C
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner.Player)
			{
				await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, player);
				await PowerCmd.Decrement(this);
			}
		}
	}
}
