using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007F5 RID: 2037
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheFutureOfPotions : EventModel
	{
		// Token: 0x17001881 RID: 6273
		// (get) Token: 0x060062D4 RID: 25300 RVA: 0x0024CE6D File Offset: 0x0024B06D
		private LocString ChoiceTitle
		{
			get
			{
				return new LocString("events", "THE_FUTURE_OF_POTIONS.pages.INITIAL.options.POTION.title");
			}
		}

		// Token: 0x17001882 RID: 6274
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x0024CE7E File Offset: 0x0024B07E
		private LocString ChoiceDescription
		{
			get
			{
				return new LocString("events", "THE_FUTURE_OF_POTIONS.pages.INITIAL.options.POTION.description");
			}
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x0024CE8F File Offset: 0x0024B08F
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Potions.Count<PotionModel>() >= 2);
		}

		// Token: 0x17001883 RID: 6275
		// (get) Token: 0x060062D7 RID: 25303 RVA: 0x0024CEBC File Offset: 0x0024B0BC
		private unsafe Dictionary<PotionModel, CardType> PotionToCardType
		{
			get
			{
				base.AssertMutable();
				if (this._cardTypes == null)
				{
					this._cardTypes = new Dictionary<PotionModel, CardType>();
					foreach (PotionModel potionModel in base.Owner.Potions)
					{
						int num = 3;
						List<CardType> list = new List<CardType>(num);
						CollectionsMarshal.SetCount<CardType>(list, num);
						Span<CardType> span = CollectionsMarshal.AsSpan<CardType>(list);
						int num2 = 0;
						*span[num2] = CardType.Attack;
						num2++;
						*span[num2] = CardType.Skill;
						num2++;
						*span[num2] = CardType.Power;
						List<CardType> list2 = list;
						if (potionModel.Rarity == PotionRarity.Common || potionModel.Rarity == PotionRarity.Token)
						{
							list2.Remove(CardType.Power);
						}
						this._cardTypes.Add(potionModel, base.Rng.NextItem<CardType>(list2));
					}
				}
				return this._cardTypes;
			}
		}

		// Token: 0x060062D8 RID: 25304 RVA: 0x0024CFAC File Offset: 0x0024B1AC
		protected override Task BeforeEventStarted()
		{
			base.Owner.CanRemovePotions = false;
			return Task.CompletedTask;
		}

		// Token: 0x060062D9 RID: 25305 RVA: 0x0024CFBF File Offset: 0x0024B1BF
		protected override void OnEventFinished()
		{
			base.Owner.CanRemovePotions = true;
		}

		// Token: 0x060062DA RID: 25306 RVA: 0x0024CFD0 File Offset: 0x0024B1D0
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			List<EventOption> list = new List<EventOption>();
			List<PotionModel> list2 = base.Owner.Potions.ToList<PotionModel>();
			int num = Mathf.Min(3, list2.Count);
			for (int i = 0; i < num; i++)
			{
				TheFutureOfPotions.<>c__DisplayClass14_0 CS$<>8__locals1 = new TheFutureOfPotions.<>c__DisplayClass14_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.potion = list2[i];
				LocString choiceTitle = this.ChoiceTitle;
				choiceTitle.Add("Rarity", CS$<>8__locals1.potion.Rarity.ToLocString().GetFormattedText());
				LocString choiceDescription = this.ChoiceDescription;
				choiceDescription.Add("Potion", CS$<>8__locals1.potion.Title.GetFormattedText());
				choiceDescription.Add("Rarity", this.GetCardRarity(CS$<>8__locals1.potion).ToLocString().GetFormattedText());
				choiceDescription.Add("Type", this.PotionToCardType[CS$<>8__locals1.potion].ToLocString().GetFormattedText());
				list.Add(new EventOption(this, delegate
				{
					TheFutureOfPotions.<>c__DisplayClass14_0.<<GenerateInitialOptions>b__0>d <<GenerateInitialOptions>b__0>d;
					<<GenerateInitialOptions>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
					<<GenerateInitialOptions>b__0>d.<>4__this = CS$<>8__locals1;
					<<GenerateInitialOptions>b__0>d.<>1__state = -1;
					<<GenerateInitialOptions>b__0>d.<>t__builder.Start<TheFutureOfPotions.<>c__DisplayClass14_0.<<GenerateInitialOptions>b__0>d>(ref <<GenerateInitialOptions>b__0>d);
					return <<GenerateInitialOptions>b__0>d.<>t__builder.Task;
				}, choiceTitle, choiceDescription, "THE_FUTURE_OF_POTIONS.pages.INITIAL.options.POTION", CS$<>8__locals1.potion.HoverTips).ThatHasDynamicTitle());
			}
			return list;
		}

		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x060062DB RID: 25307 RVA: 0x0024D100 File Offset: 0x0024B300
		public override IEnumerable<LocString> GameInfoOptions
		{
			get
			{
				List<LocString> list = base.GameInfoOptions.ToList<LocString>();
				if (list.Count != 2)
				{
					throw new InvalidOperationException("TheFutureOfPotions must've changed loc format, please update its\nGameInfoOptions method.");
				}
				LocString locString = list.First((LocString o) => o.LocEntryKey.EndsWith(".title"));
				locString.Add("Rarity", "[rarity]");
				LocString locString2 = list.First((LocString o) => o.LocEntryKey.EndsWith(".description"));
				locString2.Add("Potion", "[potion]");
				locString2.Add("Rarity", "[same-rarity]");
				locString2.Add("Type", "[card of random type]");
				return list;
			}
		}

		// Token: 0x060062DC RID: 25308 RVA: 0x0024D1BC File Offset: 0x0024B3BC
		private async Task Trade(PotionModel potion)
		{
			TheFutureOfPotions.<>c__DisplayClass17_0 CS$<>8__locals1 = new TheFutureOfPotions.<>c__DisplayClass17_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.potion = potion;
			CS$<>8__locals1.targetRarity = this.GetCardRarity(CS$<>8__locals1.potion);
			await PotionCmd.Discard(CS$<>8__locals1.potion);
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), (CardModel c) => c.Rarity == CS$<>8__locals1.targetRarity && c.Type == CS$<>8__locals1.<>4__this.PotionToCardType[CS$<>8__locals1.potion]).WithFlags(CardCreationFlags.NoRarityModification);
			CS$<>8__locals1.reward = new CardReward(cardCreationOptions, 3, base.Owner);
			CS$<>8__locals1.reward.AfterGenerated += CS$<>8__locals1.<Trade>g__UpgradeCardsInReward|1;
			await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1) { CS$<>8__locals1.reward });
			await this.Done();
		}

		// Token: 0x060062DD RID: 25309 RVA: 0x0024D207 File Offset: 0x0024B407
		private Task Done()
		{
			base.SetEventFinished(base.L10NLookup("THE_FUTURE_OF_POTIONS.pages.DONE.description"));
			return Task.CompletedTask;
		}

		// Token: 0x060062DE RID: 25310 RVA: 0x0024D220 File Offset: 0x0024B420
		private CardRarity GetCardRarity(PotionModel potion)
		{
			CardRarity cardRarity;
			switch (potion.Rarity)
			{
			case PotionRarity.Common:
			case PotionRarity.Token:
				cardRarity = CardRarity.Common;
				break;
			case PotionRarity.Uncommon:
				cardRarity = CardRarity.Uncommon;
				break;
			case PotionRarity.Rare:
			case PotionRarity.Event:
				cardRarity = CardRarity.Rare;
				break;
			default:
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(27, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Potion ");
				defaultInterpolatedStringHandler.AppendFormatted(potion.Id.Entry);
				defaultInterpolatedStringHandler.AppendLiteral(" has invalid rarity ");
				defaultInterpolatedStringHandler.AppendFormatted<PotionRarity>(potion.Rarity);
				throw new InvalidOperationException(defaultInterpolatedStringHandler.ToStringAndClear());
			}
			}
			return cardRarity;
		}

		// Token: 0x040024EE RID: 9454
		private const string _choiceKey = "THE_FUTURE_OF_POTIONS.pages.INITIAL.options.POTION";

		// Token: 0x040024EF RID: 9455
		private const string _potionKey = "Potion";

		// Token: 0x040024F0 RID: 9456
		private const string _rarityKey = "Rarity";

		// Token: 0x040024F1 RID: 9457
		private const string _typeKey = "Type";

		// Token: 0x040024F2 RID: 9458
		[Nullable(new byte[] { 2, 1 })]
		private Dictionary<PotionModel, CardType> _cardTypes;
	}
}
