using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000588 RID: 1416
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ScrollBoxes : RelicModel
	{
		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06004F1E RID: 20254 RVA: 0x0021A64F File Offset: 0x0021884F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x0021A654 File Offset: 0x00218854
		public override async Task AfterObtained()
		{
			await PlayerCmd.LoseGold(base.Owner.Gold, base.Owner, GoldLossType.Lost);
			List<IReadOnlyList<CardModel>> list = ScrollBoxes.GenerateRandomBundles(base.Owner);
			List<IReadOnlyList<CardModel>> list2 = new List<IReadOnlyList<CardModel>>();
			foreach (IReadOnlyList<CardModel> readOnlyList in list)
			{
				list2.Add(readOnlyList.Select((CardModel c) => base.Owner.RunState.CreateCard(c, base.Owner)).ToList<CardModel>());
			}
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromChooseABundleScreen(base.Owner, list2);
			foreach (CardModel cardModel in enumerable)
			{
				await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			}
			IEnumerator<CardModel> enumerator2 = null;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x0021A698 File Offset: 0x00218898
		public static bool CanGenerateBundles(Player player)
		{
			IEnumerable<CardModel> unlockedCards = player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint);
			int num = unlockedCards.Count((CardModel c) => c.Rarity == CardRarity.Common);
			int num2 = unlockedCards.Count((CardModel c) => c.Rarity == CardRarity.Uncommon);
			return num >= 4 && num2 >= 2;
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x0021A720 File Offset: 0x00218920
		public static List<IReadOnlyList<CardModel>> GenerateRandomBundles(Player player)
		{
			Rng rewards = player.PlayerRng.Rewards;
			bool flag = player.Character is Defect;
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(player.Character.CardPool), (CardModel c) => c.Rarity == CardRarity.Common).WithFlags(CardCreationFlags.NoRarityModification);
			cardCreationOptions = Hook.ModifyCardRewardCreationOptions(player.RunState, player, cardCreationOptions);
			CardCreationOptions cardCreationOptions2 = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(player.Character.CardPool), (CardModel c) => c.Rarity == CardRarity.Uncommon).WithFlags(CardCreationFlags.NoRarityModification);
			cardCreationOptions2 = Hook.ModifyCardRewardCreationOptions(player.RunState, player, cardCreationOptions2);
			List<CardModel> list = cardCreationOptions.GetPossibleCards(player).ToList<CardModel>();
			List<CardModel> list2 = cardCreationOptions2.GetPossibleCards(player).ToList<CardModel>();
			List<IReadOnlyList<CardModel>> list3 = new List<IReadOnlyList<CardModel>>();
			HashSet<ModelId> usedCardIds = new HashSet<ModelId>();
			Func<CardModel, bool> <>9__2;
			Func<CardModel, bool> <>9__3;
			for (int i = 0; i < 2; i++)
			{
				if (flag && rewards.NextInt(100) < 1)
				{
					CardModel cardModel = ModelDb.Card<Claw>();
					list3.Add(new <>z__ReadOnlyArray<CardModel>(new CardModel[] { cardModel, cardModel, cardModel }));
				}
				else
				{
					List<CardModel> list4 = new List<CardModel>();
					IEnumerable<CardModel> enumerable = list;
					Func<CardModel, bool> func;
					if ((func = <>9__2) == null)
					{
						func = (<>9__2 = (CardModel c) => !usedCardIds.Contains(c.Id));
					}
					List<CardModel> list5 = enumerable.Where(func).ToList<CardModel>();
					for (int j = 0; j < 2; j++)
					{
						CardModel cardModel2 = rewards.NextItem<CardModel>(list5);
						list4.Add(cardModel2);
						usedCardIds.Add(cardModel2.Id);
						list5.Remove(cardModel2);
					}
					IEnumerable<CardModel> enumerable2 = list2;
					Func<CardModel, bool> func2;
					if ((func2 = <>9__3) == null)
					{
						func2 = (<>9__3 = (CardModel c) => !usedCardIds.Contains(c.Id));
					}
					List<CardModel> list6 = enumerable2.Where(func2).ToList<CardModel>();
					CardModel cardModel3 = rewards.NextItem<CardModel>(list6);
					list4.Add(cardModel3);
					usedCardIds.Add(cardModel3.Id);
					list3.Add(list4);
				}
			}
			return list3;
		}

		// Token: 0x04002212 RID: 8722
		private const int _clawBundleChancePercent = 1;
	}
}
