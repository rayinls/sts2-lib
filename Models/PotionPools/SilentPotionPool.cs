using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000726 RID: 1830
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SilentPotionPool : PotionPoolModel
	{
		// Token: 0x17001498 RID: 5272
		// (get) Token: 0x060058CC RID: 22732 RVA: 0x0022BA40 File Offset: 0x00229C40
		public override string EnergyColorName
		{
			get
			{
				return "silent";
			}
		}

		// Token: 0x17001499 RID: 5273
		// (get) Token: 0x060058CD RID: 22733 RVA: 0x0022BA47 File Offset: 0x00229C47
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.green;
			}
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x0022BA4E File Offset: 0x00229C4E
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return Silent4Epoch.Potions;
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x0022BA58 File Offset: 0x00229C58
		public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			if (!unlockState.IsEpochRevealed<Silent4Epoch>())
			{
				return Array.Empty<PotionModel>();
			}
			return this.GenerateAllPotions();
		}
	}
}
