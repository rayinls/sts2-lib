using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.RelicPools
{
	// Token: 0x020005D1 RID: 1489
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SilentRelicPool : RelicPoolModel
	{
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x0021E874 File Offset: 0x0021CA74
		public override string EnergyColorName
		{
			get
			{
				return "silent";
			}
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x0021E87B File Offset: 0x0021CA7B
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.green;
			}
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x0021E884 File Offset: 0x0021CA84
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlyArray<RelicModel>(new RelicModel[]
			{
				ModelDb.Relic<HelicalDart>(),
				ModelDb.Relic<NinjaScroll>(),
				ModelDb.Relic<PaperKrane>(),
				ModelDb.Relic<RingOfTheSnake>(),
				ModelDb.Relic<SneckoSkull>(),
				ModelDb.Relic<Tingsha>(),
				ModelDb.Relic<ToughBandages>(),
				ModelDb.Relic<TwistedFunnel>()
			});
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x0021E8DC File Offset: 0x0021CADC
		public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
		{
			List<RelicModel> list = base.AllRelics.ToList<RelicModel>();
			if (!unlockState.IsEpochRevealed<Silent3Epoch>())
			{
				list.RemoveAll((RelicModel r) => Silent3Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			if (!unlockState.IsEpochRevealed<Silent6Epoch>())
			{
				list.RemoveAll((RelicModel r) => Silent6Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			return list;
		}
	}
}
