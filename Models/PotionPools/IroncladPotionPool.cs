using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;

namespace MegaCrit.Sts2.Core.Models.PotionPools
{
	// Token: 0x02000722 RID: 1826
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IroncladPotionPool : PotionPoolModel
	{
		// Token: 0x17001491 RID: 5265
		// (get) Token: 0x060058B9 RID: 22713 RVA: 0x0022B712 File Offset: 0x00229912
		public override string EnergyColorName
		{
			get
			{
				return "ironclad";
			}
		}

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060058BA RID: 22714 RVA: 0x0022B719 File Offset: 0x00229919
		public override Color LabOutlineColor
		{
			get
			{
				return StsColors.red;
			}
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x0022B720 File Offset: 0x00229920
		protected override IEnumerable<PotionModel> GenerateAllPotions()
		{
			return Ironclad4Epoch.Potions;
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x0022B728 File Offset: 0x00229928
		public override IEnumerable<PotionModel> GetUnlockedPotions(UnlockState unlockState)
		{
			if (!unlockState.IsEpochRevealed<Ironclad4Epoch>())
			{
				return Array.Empty<PotionModel>();
			}
			return this.GenerateAllPotions();
		}
	}
}
