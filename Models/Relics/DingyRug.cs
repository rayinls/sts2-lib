using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E4 RID: 1252
	public sealed class DingyRug : RelicModel
	{
		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06004AF4 RID: 19188 RVA: 0x00212E42 File Offset: 0x00211042
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00212E48 File Offset: 0x00211048
		[NullableContext(1)]
		public override CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
		{
			if (base.Owner != player)
			{
				return options;
			}
			if (options.Flags.HasFlag(CardCreationFlags.NoCardPoolModifications))
			{
				return options;
			}
			List<CardModel> list = options.GetPossibleCards(player).ToList<CardModel>();
			CardPoolModel cardPoolModel = ModelDb.CardPool<ColorlessCardPool>();
			List<CardModel> list2 = cardPoolModel.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint).ToList<CardModel>();
			if (options.Flags.HasFlag(CardCreationFlags.NoRarityModification))
			{
				HashSet<CardRarity> allowedRarities = (from c in options.GetPossibleCards(player)
					select c.Rarity).ToHashSet<CardRarity>();
				list2 = list2.Where((CardModel c) => allowedRarities.Contains(c.Rarity)).ToList<CardModel>();
			}
			foreach (CardModel cardModel in list2)
			{
				if (!list.Contains(cardModel))
				{
					list.Add(cardModel);
				}
			}
			return options.WithCustomPool(list, null);
		}
	}
}
