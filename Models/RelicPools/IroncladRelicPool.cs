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
	// Token: 0x020005CD RID: 1485
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IroncladRelicPool : RelicPoolModel
	{
		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x0021E07E File Offset: 0x0021C27E
		public override string EnergyColorName
		{
			get
			{
				return "ironclad";
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x060050D6 RID: 20694 RVA: 0x0021E085 File Offset: 0x0021C285
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.red;
			}
		}

		// Token: 0x060050D7 RID: 20695 RVA: 0x0021E08C File Offset: 0x0021C28C
		protected override IEnumerable<RelicModel> GenerateAllRelics()
		{
			return new <>z__ReadOnlyArray<RelicModel>(new RelicModel[]
			{
				ModelDb.Relic<Brimstone>(),
				ModelDb.Relic<BurningBlood>(),
				ModelDb.Relic<CharonsAshes>(),
				ModelDb.Relic<DemonTongue>(),
				ModelDb.Relic<PaperPhrog>(),
				ModelDb.Relic<RedSkull>(),
				ModelDb.Relic<RuinedHelmet>(),
				ModelDb.Relic<SelfFormingClay>()
			});
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x0021E0E4 File Offset: 0x0021C2E4
		public override IEnumerable<RelicModel> GetUnlockedRelics(UnlockState unlockState)
		{
			List<RelicModel> list = base.AllRelics.ToList<RelicModel>();
			if (!unlockState.IsEpochRevealed<Ironclad3Epoch>())
			{
				list.RemoveAll((RelicModel r) => Ironclad3Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			if (!unlockState.IsEpochRevealed<Ironclad6Epoch>())
			{
				list.RemoveAll((RelicModel r) => Ironclad6Epoch.Relics.Any((RelicModel relic) => relic.Id == r.Id));
			}
			return list;
		}
	}
}
