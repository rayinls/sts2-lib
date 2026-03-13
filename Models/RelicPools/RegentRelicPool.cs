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
	// Token: 0x020005CF RID: 1487
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RegentRelicPool : RelicPoolModel
	{
		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x060050DF RID: 20703 RVA: 0x0021E246 File Offset: 0x0021C446
		public override string EnergyColorName
		{
			get
			{
				return "regent";
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x060050E0 RID: 20704 RVA: 0x0021E24D File Offset: 0x0021C44D
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.orange;
			}
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0021E254 File Offset: 0x0021C454
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlyArray<RelicModel>(new RelicModel[]
			{
				ModelDb.Relic<DivineRight>(),
				ModelDb.Relic<FencingManual>(),
				ModelDb.Relic<GalacticDust>(),
				ModelDb.Relic<LunarPastry>(),
				ModelDb.Relic<MiniRegent>(),
				ModelDb.Relic<OrangeDough>(),
				ModelDb.Relic<Regalite>(),
				ModelDb.Relic<VitruvianMinion>()
			});
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0021E2AC File Offset: 0x0021C4AC
		public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
		{
			List<RelicModel> list = base.AllRelics.ToList<RelicModel>();
			if (!unlockState.IsEpochRevealed<Regent3Epoch>())
			{
				list.RemoveAll((RelicModel r) => Regent3Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			if (!unlockState.IsEpochRevealed<Regent6Epoch>())
			{
				list.RemoveAll((RelicModel r) => Regent6Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			return list;
		}
	}
}
