using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000616 RID: 1558
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EnergyNextTurnPower : PowerModel
	{
		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x002211B5 File Offset: 0x0021F3B5
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06005271 RID: 21105 RVA: 0x002211B8 File Offset: 0x0021F3B8
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06005272 RID: 21106 RVA: 0x002211BB File Offset: 0x0021F3BB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x002211C8 File Offset: 0x0021F3C8
		public override async Task AfterEnergyReset(Player player)
		{
			if (player == base.Owner.Player)
			{
				await PlayerCmd.GainEnergy(base.Amount, player);
				await PowerCmd.Remove(this);
			}
		}
	}
}
