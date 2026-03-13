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
	// Token: 0x020005CE RID: 1486
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NecrobinderRelicPool : RelicPoolModel
	{
		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x060050DA RID: 20698 RVA: 0x0021E162 File Offset: 0x0021C362
		public override string EnergyColorName
		{
			get
			{
				return "necrobinder";
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x060050DB RID: 20699 RVA: 0x0021E169 File Offset: 0x0021C369
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.pink;
			}
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x0021E170 File Offset: 0x0021C370
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlyArray<RelicModel>(new RelicModel[]
			{
				ModelDb.Relic<BigHat>(),
				ModelDb.Relic<BoneFlute>(),
				ModelDb.Relic<BookRepairKnife>(),
				ModelDb.Relic<Bookmark>(),
				ModelDb.Relic<BoundPhylactery>(),
				ModelDb.Relic<FuneraryMask>(),
				ModelDb.Relic<IvoryTile>(),
				ModelDb.Relic<UndyingSigil>()
			});
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0021E1C8 File Offset: 0x0021C3C8
		public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
		{
			List<RelicModel> list = base.AllRelics.ToList<RelicModel>();
			if (!unlockState.IsEpochRevealed<Necrobinder3Epoch>())
			{
				list.RemoveAll((RelicModel r) => Necrobinder3Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			if (!unlockState.IsEpochRevealed<Necrobinder6Epoch>())
			{
				list.RemoveAll((RelicModel r) => Necrobinder6Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			return list;
		}
	}
}
