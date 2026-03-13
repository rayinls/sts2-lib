using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000550 RID: 1360
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Orrery : RelicModel
	{
		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06004DA7 RID: 19879 RVA: 0x00217C89 File Offset: 0x00215E89
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06004DA8 RID: 19880 RVA: 0x00217C8C File Offset: 0x00215E8C
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06004DA9 RID: 19881 RVA: 0x00217C8F File Offset: 0x00215E8F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(5));
			}
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00217C9C File Offset: 0x00215E9C
		public override async Task AfterObtained()
		{
			List<Reward> list = new List<Reward>();
			CardCreationOptions cardCreationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), CardCreationSource.Other, CardRarityOddsType.RegularEncounter, null);
			for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++)
			{
				list.Add(new CardReward(cardCreationOptions, 3, base.Owner));
			}
			await RewardsCmd.OfferCustom(base.Owner, list);
		}
	}
}
