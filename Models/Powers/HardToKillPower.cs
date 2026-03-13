using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000639 RID: 1593
	public sealed class HardToKillPower : PowerModel
	{
		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x0600532C RID: 21292 RVA: 0x00222592 File Offset: 0x00220792
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x0600532D RID: 21293 RVA: 0x00222595 File Offset: 0x00220795
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600532E RID: 21294 RVA: 0x00222598 File Offset: 0x00220798
		[NullableContext(2)]
		public override decimal ModifyDamageCap(Creature target, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return decimal.MaxValue;
			}
			return base.Amount;
		}

		// Token: 0x0600532F RID: 21295 RVA: 0x002225B9 File Offset: 0x002207B9
		[NullableContext(1)]
		public override Task AfterModifyingDamageAmount([Nullable(2)] CardModel cardSource)
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
