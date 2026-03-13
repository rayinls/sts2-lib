using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Rewards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005A9 RID: 1449
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TinyMailbox : RelicModel
	{
		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06004FEE RID: 20462 RVA: 0x0021BFAF File Offset: 0x0021A1AF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x0021BFB2 File Offset: 0x0021A1B2
		public override bool TryModifyRestSiteHealRewards(Player player, List<Reward> rewards, bool isMimicked)
		{
			if (player != base.Owner)
			{
				return false;
			}
			rewards.Add(new PotionReward(player));
			base.Flash();
			return true;
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0021BFD4 File Offset: 0x0021A1D4
		public override IReadOnlyList<LocString> ModifyExtraRestSiteHealText(Player player, IReadOnlyList<LocString> currentExtraText)
		{
			if (!LocalContext.IsMe(base.Owner))
			{
				return currentExtraText;
			}
			int num = 0;
			LocString[] array = new LocString[1 + currentExtraText.Count];
			foreach (LocString locString in currentExtraText)
			{
				array[num] = locString;
				num++;
			}
			array[num] = base.AdditionalRestSiteHealText;
			return new <>z__ReadOnlyArray<LocString>(array);
		}
	}
}
