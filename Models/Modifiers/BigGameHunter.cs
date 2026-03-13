using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007AD RID: 1965
	[NullableContext(1)]
	[Nullable(0)]
	public class BigGameHunter : ModifierModel
	{
		// Token: 0x060060C2 RID: 24770 RVA: 0x0024356C File Offset: 0x0024176C
		public override ActMap ModifyGeneratedMap(IRunState runState, ActMap map, int actIndex)
		{
			uint seed = runState.Rng.Seed;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
			defaultInterpolatedStringHandler.AppendLiteral("act_");
			defaultInterpolatedStringHandler.AppendFormatted<int>(runState.CurrentActIndex + 1);
			defaultInterpolatedStringHandler.AppendLiteral("_map");
			Rng rng = new Rng(seed, defaultInterpolatedStringHandler.ToStringAndClear());
			MapPointTypeCounts mapPointTypeCounts = new MapPointTypeCounts(rng);
			mapPointTypeCounts.NumOfElites = (int)Math.Round((double)((float)map.GetAllMapPoints().Count((MapPoint p) => p.PointType == MapPointType.Elite) * 2.5f));
			mapPointTypeCounts.PointTypesThatIgnoreRules = new HashSet<MapPointType> { MapPointType.Elite };
			MapPointTypeCounts mapPointTypeCounts2 = mapPointTypeCounts;
			StandardActMap standardActMap = map as StandardActMap;
			bool flag = standardActMap != null && standardActMap.ShouldReplaceTreasureWithElites;
			return new StandardActMap(rng, runState.Act, runState.Players.Count > 1, flag, runState.Act.HasSecondBoss, mapPointTypeCounts2, true);
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x00243658 File Offset: 0x00241858
		public override CardCreationOptions ModifyCardRewardCreationOptions(Player player, CardCreationOptions options)
		{
			if (options.Source != CardCreationSource.Encounter || options.RarityOdds != CardRarityOddsType.EliteEncounter)
			{
				return options;
			}
			if (options.Flags.HasFlag(CardCreationFlags.NoCardPoolModifications) || options.Flags.HasFlag(CardCreationFlags.NoRarityModification))
			{
				return options;
			}
			List<CardModel> list = (from c in options.GetPossibleCards(player)
				where c.Rarity == CardRarity.Rare
				select c).ToList<CardModel>();
			if (list.Count <= 0)
			{
				list = (from c in player.Character.CardPool.GetUnlockedCards(player.UnlockState, player.RunState.CardMultiplayerConstraint)
					where c.Rarity == CardRarity.Rare
					select c).ToList<CardModel>();
			}
			return options.WithCustomPool(list, new CardRarityOddsType?(CardRarityOddsType.Uniform));
		}
	}
}
