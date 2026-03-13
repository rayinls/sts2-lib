using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C8 RID: 1992
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ColorfulPhilosophers : EventModel
	{
		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x0600612B RID: 24875 RVA: 0x00244B37 File Offset: 0x00242D37
		private static IEnumerable<CardPoolModel> CardPoolColorOrder
		{
			get
			{
				return new <>z__ReadOnlyArray<CardPoolModel>(new CardPoolModel[]
				{
					ModelDb.CardPool<NecrobinderCardPool>(),
					ModelDb.CardPool<IroncladCardPool>(),
					ModelDb.CardPool<RegentCardPool>(),
					ModelDb.CardPool<SilentCardPool>(),
					ModelDb.CardPool<DefectCardPool>()
				});
			}
		}

		// Token: 0x17001803 RID: 6147
		// (get) Token: 0x0600612C RID: 24876 RVA: 0x00244B6C File Offset: 0x00242D6C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x0600612D RID: 24877 RVA: 0x00244B7C File Offset: 0x00242D7C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = new List<EventOption>();
			CharacterModel character = base.Owner.Character;
			List<CardPoolModel> list2 = base.Owner.UnlockState.CharacterCardPools.ToList<CardPoolModel>();
			using (IEnumerator<CardPoolModel> enumerator = ColorfulPhilosophers.CardPoolColorOrder.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CardPoolModel cardPool = enumerator.Current;
					if (character.CardPool != cardPool && list2.Contains(cardPool))
					{
						list.Add(new EventOption(this, () => this.OfferRewards(cardPool), "COLORFUL_PHILOSOPHERS.pages.INITIAL.options." + cardPool.EnergyColorName.ToUpperInvariant(), Array.Empty<IHoverTip>()));
					}
				}
			}
			int num = Mathf.Min(3, list.Count);
			while (list.Count > num)
			{
				list.RemoveAt(base.Rng.NextInt(list.Count));
			}
			return list;
		}

		// Token: 0x0600612E RID: 24878 RVA: 0x00244C8C File Offset: 0x00242E8C
		private async Task OfferRewards(CardPoolModel pool)
		{
			CardCreationOptions cardCreationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(pool), CardCreationSource.Other, CardRarityOddsType.Uniform, (CardModel c) => c.Rarity == CardRarity.Common).WithFlags(CardCreationFlags.NoRarityModification);
			CardCreationOptions cardCreationOptions2 = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(pool), CardCreationSource.Other, CardRarityOddsType.Uniform, (CardModel c) => c.Rarity == CardRarity.Uncommon).WithFlags(CardCreationFlags.NoRarityModification);
			CardCreationOptions cardCreationOptions3 = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(pool), CardCreationSource.Other, CardRarityOddsType.Uniform, (CardModel c) => c.Rarity == CardRarity.Rare).WithFlags(CardCreationFlags.NoRarityModification);
			await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(3)
			{
				new CardReward(cardCreationOptions, base.DynamicVars.Cards.IntValue, base.Owner),
				new CardReward(cardCreationOptions2, base.DynamicVars.Cards.IntValue, base.Owner),
				new CardReward(cardCreationOptions3, base.DynamicVars.Cards.IntValue, base.Owner)
			});
			base.SetEventFinished(base.L10NLookup("COLORFUL_PHILOSOPHERS.pages.DONE.description"));
		}
	}
}
