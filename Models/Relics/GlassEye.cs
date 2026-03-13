using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200050B RID: 1291
	public sealed class GlassEye : RelicModel
	{
		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06004BEC RID: 19436 RVA: 0x00214B20 File Offset: 0x00212D20
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00214B24 File Offset: 0x00212D24
		[NullableContext(1)]
		public override async Task AfterObtained()
		{
			List<Reward> list = new List<Reward>();
			CardRarity[] array = new CardRarity[]
			{
				CardRarity.Common,
				CardRarity.Common,
				CardRarity.Uncommon,
				CardRarity.Uncommon,
				CardRarity.Rare
			};
			CardRarity[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				CardRarity rarity = array2[i];
				CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), (CardModel c) => c.Rarity == rarity).WithFlags(CardCreationFlags.NoRarityModification);
				list.Add(new CardReward(cardCreationOptions, 3, base.Owner));
			}
			await RewardsCmd.OfferCustom(base.Owner, list);
		}
	}
}
