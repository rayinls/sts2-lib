using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.RelicPools
{
	// Token: 0x020005CA RID: 1482
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DeprecatedRelicPool : RelicPoolModel
	{
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x060050CC RID: 20684 RVA: 0x0021DB76 File Offset: 0x0021BD76
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x060050CD RID: 20685 RVA: 0x0021DB7D File Offset: 0x0021BD7D
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<DeprecatedRelic>());
		}
	}
}
