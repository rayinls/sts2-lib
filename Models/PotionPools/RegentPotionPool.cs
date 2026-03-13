using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000724 RID: 1828
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RegentPotionPool : PotionPoolModel
	{
		// Token: 0x17001495 RID: 5269
		// (get) Token: 0x060058C3 RID: 22723 RVA: 0x0022B793 File Offset: 0x00229993
		public override string EnergyColorName
		{
			get
			{
				return "regent";
			}
		}

		// Token: 0x17001496 RID: 5270
		// (get) Token: 0x060058C4 RID: 22724 RVA: 0x0022B79A File Offset: 0x0022999A
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.orange;
			}
		}

		// Token: 0x060058C5 RID: 22725 RVA: 0x0022B7A1 File Offset: 0x002299A1
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return Regent4Epoch.Potions;
		}

		// Token: 0x060058C6 RID: 22726 RVA: 0x0022B7A8 File Offset: 0x002299A8
		public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			if (!unlockState.IsEpochRevealed<Regent4Epoch>())
			{
				return Array.Empty<PotionModel>();
			}
			return this.GenerateAllPotions();
		}
	}
}
