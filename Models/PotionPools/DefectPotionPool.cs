using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x0200071F RID: 1823
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DefectPotionPool : PotionPoolModel
	{
		// Token: 0x1700148D RID: 5261
		// (get) Token: 0x060058AE RID: 22702 RVA: 0x0022B68B File Offset: 0x0022988B
		public override string EnergyColorName
		{
			get
			{
				return "defect";
			}
		}

		// Token: 0x1700148E RID: 5262
		// (get) Token: 0x060058AF RID: 22703 RVA: 0x0022B692 File Offset: 0x00229892
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.blue;
			}
		}

		// Token: 0x060058B0 RID: 22704 RVA: 0x0022B699 File Offset: 0x00229899
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return Defect4Epoch.Potions;
		}

		// Token: 0x060058B1 RID: 22705 RVA: 0x0022B6A0 File Offset: 0x002298A0
		public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			if (!unlockState.IsEpochRevealed<Defect4Epoch>())
			{
				return Array.Empty<PotionModel>();
			}
			return this.GenerateAllPotions();
		}
	}
}
