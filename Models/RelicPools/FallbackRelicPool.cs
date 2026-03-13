using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Models.Relics;

namespace MegaCrit.Sts2.Core.Models.RelicPools
{
	// Token: 0x020005CC RID: 1484
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FallbackRelicPool : RelicPoolModel
	{
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x060050D2 RID: 20690 RVA: 0x0021E063 File Offset: 0x0021C263
		public override string EnergyColorName
		{
			get
			{
				return "colorless";
			}
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x0021E06A File Offset: 0x0021C26A
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlySingleElementList<RelicModel>(ModelDb.Relic<Circlet>());
		}
	}
}
