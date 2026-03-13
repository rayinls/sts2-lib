using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000723 RID: 1827
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NecrobinderPotionPool : PotionPoolModel
	{
		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060058BE RID: 22718 RVA: 0x0022B753 File Offset: 0x00229953
		public override string EnergyColorName
		{
			get
			{
				return "necrobinder";
			}
		}

		// Token: 0x17001494 RID: 5268
		// (get) Token: 0x060058BF RID: 22719 RVA: 0x0022B75A File Offset: 0x0022995A
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.pink;
			}
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x0022B761 File Offset: 0x00229961
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return Necrobinder4Epoch.Potions;
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x0022B768 File Offset: 0x00229968
		public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			if (!unlockState.IsEpochRevealed<Necrobinder4Epoch>())
			{
				return Array.Empty<PotionModel>();
			}
			return this.GenerateAllPotions();
		}
	}
}
