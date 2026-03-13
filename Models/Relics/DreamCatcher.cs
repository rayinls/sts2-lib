using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004EA RID: 1258
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DreamCatcher : RelicModel
	{
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06004B0F RID: 19215 RVA: 0x002131A3 File Offset: 0x002113A3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x002131A6 File Offset: 0x002113A6
		public override bool TryModifyRestSiteHealRewards(Player player, List<Reward> rewards, bool isMimicked)
		{
			if (player != base.Owner)
			{
				return false;
			}
			rewards.Add(new CardReward(CardCreationOptions.ForRoom(player, RoomType.Monster), 3, base.Owner));
			base.Flash();
			return true;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x002131D4 File Offset: 0x002113D4
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
