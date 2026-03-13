using System;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000659 RID: 1625
	public sealed class MinionPower : PowerModel
	{
		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060053D8 RID: 21464 RVA: 0x002238FB File Offset: 0x00221AFB
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060053D9 RID: 21465 RVA: 0x002238FE File Offset: 0x00221AFE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060053DA RID: 21466 RVA: 0x00223901 File Offset: 0x00221B01
		public override bool ShouldPlayVfx
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x060053DB RID: 21467 RVA: 0x00223904 File Offset: 0x00221B04
		public override bool OwnerIsSecondaryEnemy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060053DC RID: 21468 RVA: 0x00223907 File Offset: 0x00221B07
		public override bool ShouldPowerBeRemovedAfterOwnerDeath()
		{
			return false;
		}

		// Token: 0x060053DD RID: 21469 RVA: 0x0022390A File Offset: 0x00221B0A
		public override bool ShouldOwnerDeathTriggerFatal()
		{
			return false;
		}
	}
}
