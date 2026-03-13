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
	// Token: 0x020005C9 RID: 1481
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefectRelicPool : RelicPoolModel
	{
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x060050C7 RID: 20679 RVA: 0x0021DA8F File Offset: 0x0021BC8F
		public override string EnergyColorName
		{
			get
			{
				return "defect";
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x060050C8 RID: 20680 RVA: 0x0021DA96 File Offset: 0x0021BC96
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.blue;
			}
		}

		// Token: 0x060050C9 RID: 20681 RVA: 0x0021DAA0 File Offset: 0x0021BCA0
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlyArray<RelicModel>(new RelicModel[]
			{
				ModelDb.Relic<CrackedCore>(),
				ModelDb.Relic<DataDisk>(),
				ModelDb.Relic<EmotionChip>(),
				ModelDb.Relic<GoldPlatedCables>(),
				ModelDb.Relic<PowerCell>(),
				ModelDb.Relic<Metronome>(),
				ModelDb.Relic<RunicCapacitor>(),
				ModelDb.Relic<SymbioticVirus>()
			});
		}

		// Token: 0x060050CA RID: 20682 RVA: 0x0021DAF8 File Offset: 0x0021BCF8
		public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
		{
			List<RelicModel> list = base.AllRelics.ToList<RelicModel>();
			if (!unlockState.IsEpochRevealed<Defect3Epoch>())
			{
				list.RemoveAll((RelicModel r) => Defect3Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			if (!unlockState.IsEpochRevealed<Defect6Epoch>())
			{
				list.RemoveAll((RelicModel r) => Defect6Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			return list;
		}
	}
}
