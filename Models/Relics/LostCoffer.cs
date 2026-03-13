using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000530 RID: 1328
	public sealed class LostCoffer : RelicModel
	{
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x0021679B File Offset: 0x0021499B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x0021679E File Offset: 0x0021499E
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x002167A4 File Offset: 0x002149A4
		[NullableContext(1)]
		public override async Task AfterObtained()
		{
			List<Reward> list = new List<Reward>();
			CardCreationOptions cardCreationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), CardCreationSource.Other, CardRarityOddsType.RegularEncounter, null);
			list.Add(new CardReward(cardCreationOptions, 3, base.Owner));
			list.Add(new PotionReward(base.Owner));
			await RewardsCmd.OfferCustom(base.Owner, list);
		}
	}
}
