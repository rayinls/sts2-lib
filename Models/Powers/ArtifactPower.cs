using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D9 RID: 1497
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ArtifactPower : PowerModel
	{
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06005112 RID: 20754 RVA: 0x0021EC17 File Offset: 0x0021CE17
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06005113 RID: 20755 RVA: 0x0021EC1A File Offset: 0x0021CE1A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x0021EC1D File Offset: 0x0021CE1D
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x0021EC20 File Offset: 0x0021CE20
		public override bool TryModifyPowerAmountReceived(PowerModel canonicalPower, Creature target, decimal amount, [Nullable(2)] Creature _, out decimal modifiedAmount)
		{
			if (target != base.Owner)
			{
				modifiedAmount = amount;
				return false;
			}
			if (canonicalPower.GetTypeForAmount(amount) != PowerType.Debuff)
			{
				modifiedAmount = amount;
				return false;
			}
			if (!canonicalPower.IsVisible)
			{
				modifiedAmount = amount;
				return false;
			}
			modifiedAmount = 0m;
			return true;
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0021EC70 File Offset: 0x0021CE70
		public override async Task AfterModifyingPowerAmountReceived(PowerModel power)
		{
			await PowerCmd.Decrement(this);
		}
	}
}
