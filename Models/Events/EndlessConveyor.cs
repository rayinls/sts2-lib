using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007D2 RID: 2002
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EndlessConveyor : EventModel
	{
		// Token: 0x06006175 RID: 24949 RVA: 0x002460A3 File Offset: 0x002442A3
		public override bool IsAllowed(RunState runState)
		{
			return runState.Players.All((Player p) => p.Gold >= 105);
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06006176 RID: 24950 RVA: 0x002460CF File Offset: 0x002442CF
		// (set) Token: 0x06006177 RID: 24951 RVA: 0x002460D7 File Offset: 0x002442D7
		private int NumOfGrabs
		{
			get
			{
				return this._numOfGrabs;
			}
			set
			{
				base.AssertMutable();
				this._numOfGrabs = value;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06006178 RID: 24952 RVA: 0x002460E6 File Offset: 0x002442E6
		// (set) Token: 0x06006179 RID: 24953 RVA: 0x002460EE File Offset: 0x002442EE
		private EndlessConveyor.Dish CurrentDish
		{
			get
			{
				return this._currentDish;
			}
			set
			{
				base.AssertMutable();
				this._currentDish = value;
			}
		}

		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x0600617A RID: 24954 RVA: 0x00246100 File Offset: 0x00244300
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new GoldVar(35),
					new GoldVar("GoldenFyshGold", 75),
					new HealVar("ClamRollHeal", 10m),
					new MaxHpVar("CaviarMaxHp", 4m),
					new StringVar("CurrentDishTitle", ""),
					new StringVar("LastDishTitle", "")
				});
			}
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x0024617C File Offset: 0x0024437C
		public override void CalculateVars()
		{
			this.RollDish();
			((StringVar)base.DynamicVars["CurrentDishTitle"]).StringValue = this.CurrentDish.title.GetFormattedText();
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x002461AE File Offset: 0x002443AE
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				this.GenerateGrabSomethingOffTheBeltOption(),
				new EventOption(this, new Func<Task>(this.ObserveChef), "ENDLESS_CONVEYOR.pages.INITIAL.options.OBSERVE_CHEF", Array.Empty<IHoverTip>())
			});
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x002461E4 File Offset: 0x002443E4
		private async Task GrabSomethingOffTheBelt()
		{
			if (this._currentDish.id != "GOLDEN_FYSH")
			{
				await PlayerCmd.LoseGold(base.DynamicVars.Gold.IntValue, base.Owner, GoldLossType.Spent);
			}
			await this.CurrentDish.action();
			this.RollDish();
			((StringVar)base.DynamicVars["LastDishTitle"]).StringValue = ((StringVar)base.DynamicVars["CurrentDishTitle"]).StringValue;
			((StringVar)base.DynamicVars["CurrentDishTitle"]).StringValue = this.CurrentDish.title.GetFormattedText();
			this.SetEventState(base.L10NLookup("ENDLESS_CONVEYOR.pages.GRAB_SOMETHING_OFF_THE_BELT.description"), new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				this.GenerateGrabSomethingOffTheBeltOption(),
				new EventOption(this, new Func<Task>(this.Leave), "ENDLESS_CONVEYOR.pages.GRAB_SOMETHING_OFF_THE_BELT.options.LEAVE", Array.Empty<IHoverTip>())
			}));
		}

		// Token: 0x0600617E RID: 24958 RVA: 0x00246228 File Offset: 0x00244428
		private EventOption GenerateGrabSomethingOffTheBeltOption()
		{
			if (base.Owner.Gold >= base.DynamicVars.Gold.IntValue)
			{
				return new EventOption(this, new Func<Task>(this.GrabSomethingOffTheBelt), this._currentDish.optionKey, this._currentDish.hoverTips);
			}
			return new EventOption(this, null, "ENDLESS_CONVEYOR.pages.ALL.options.LOCKED", Array.Empty<IHoverTip>());
		}

		// Token: 0x0600617F RID: 24959 RVA: 0x0024628C File Offset: 0x0024448C
		private async Task ClamRoll()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars["ClamRollHeal"].IntValue, true);
		}

		// Token: 0x06006180 RID: 24960 RVA: 0x002462D0 File Offset: 0x002444D0
		private async Task Caviar()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars["CaviarMaxHp"].IntValue);
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x00246314 File Offset: 0x00244514
		private async Task SuspiciousCondiment()
		{
			IEnumerable<PotionModel> enumerable = base.Owner.Character.PotionPool.GetUnlockedPotions(base.Owner.UnlockState).Concat(ModelDb.PotionPool<SharedPotionPool>().GetUnlockedPotions(base.Owner.UnlockState));
			PotionModel potionModel = base.Owner.PlayerRng.Rewards.NextItem<PotionModel>(enumerable);
			if (potionModel != null)
			{
				await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
				{
					new PotionReward(potionModel.ToMutable(), base.Owner)
				});
			}
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x00246358 File Offset: 0x00244558
		private async Task JellyLiver()
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForTransformation(base.Owner, new CardSelectorPrefs(CardSelectorPrefs.TransformSelectionPrompt, 1), null);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.TransformToRandom(cardModel, base.Rng, CardPreviewStyle.EventLayout);
			}
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x0024639C File Offset: 0x0024459C
		private async Task SeapunkSalad()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<FeedingFrenzy>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.EventLayout);
		}

		// Token: 0x06006184 RID: 24964 RVA: 0x002463E0 File Offset: 0x002445E0
		private async Task FriedEel()
		{
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithDefaultOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(ModelDb.CardPool<ColorlessCardPool>()), null);
			CardModel card = CardFactory.CreateForReward(base.Owner, 1, cardCreationOptions).First<CardCreationResult>().Card;
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.EventLayout);
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x00246424 File Offset: 0x00244624
		private async Task GoldenFysh()
		{
			await PlayerCmd.GainGold(base.DynamicVars["GoldenFyshGold"].BaseValue, base.Owner, false);
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x00246468 File Offset: 0x00244668
		private Task SpicySnappy()
		{
			List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).ToList<CardModel>();
			if (list.Count != 0)
			{
				CardModel cardModel = base.Rng.NextItem<CardModel>(list);
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
			return Task.CompletedTask;
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x002464D4 File Offset: 0x002446D4
		private unsafe void RollDish()
		{
			int num = this.NumOfGrabs;
			this.NumOfGrabs = num + 1;
			if (this.NumOfGrabs % 5 == 0)
			{
				this._lastDishId = "SEAPUNK_SALAD";
				this._currentDish = new EndlessConveyor.Dish("SEAPUNK_SALAD", new Func<Task>(this.SeapunkSalad), HoverTipFactory.FromCardWithCardHoverTips<FeedingFrenzy>(false), 0f);
				return;
			}
			num = 4;
			List<EndlessConveyor.Dish> list = new List<EndlessConveyor.Dish>(num);
			CollectionsMarshal.SetCount<EndlessConveyor.Dish>(list, num);
			Span<EndlessConveyor.Dish> span = CollectionsMarshal.AsSpan<EndlessConveyor.Dish>(list);
			int num2 = 0;
			*span[num2] = new EndlessConveyor.Dish("CAVIAR", new Func<Task>(this.Caviar), Array.Empty<IHoverTip>(), 6f);
			num2++;
			*span[num2] = new EndlessConveyor.Dish("SPICY_SNAPPY", new Func<Task>(this.SpicySnappy), Array.Empty<IHoverTip>(), 3f);
			num2++;
			*span[num2] = new EndlessConveyor.Dish("JELLY_LIVER", new Func<Task>(this.JellyLiver), new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Transform, Array.Empty<DynamicVar>())), 3f);
			num2++;
			*span[num2] = new EndlessConveyor.Dish("FRIED_EEL", new Func<Task>(this.FriedEel), Array.Empty<IHoverTip>(), 3f);
			List<EndlessConveyor.Dish> list2 = list;
			if (base.Owner.HasOpenPotionSlots)
			{
				list2.Add(new EndlessConveyor.Dish("SUSPICIOUS_CONDIMENT", new Func<Task>(this.SuspiciousCondiment), Array.Empty<IHoverTip>(), 3f));
			}
			if (base.Owner.Creature.CurrentHp != base.Owner.Creature.MaxHp)
			{
				list2.Add(new EndlessConveyor.Dish("CLAM_ROLL", new Func<Task>(this.ClamRoll), Array.Empty<IHoverTip>(), 6f));
			}
			if (this.NumOfGrabs > 1)
			{
				list2.Add(new EndlessConveyor.Dish("GOLDEN_FYSH", new Func<Task>(this.GoldenFysh), Array.Empty<IHoverTip>(), 1f));
			}
			list2.RemoveAll((EndlessConveyor.Dish d) => d.id == this._lastDishId);
			float num3 = 0f;
			foreach (EndlessConveyor.Dish dish in list2)
			{
				num3 += dish.weight;
			}
			float num4 = base.Rng.NextFloat(1f) * num3;
			float num5 = 0f;
			foreach (EndlessConveyor.Dish dish2 in list2)
			{
				num5 += dish2.weight;
				if (num4 < num5)
				{
					this._lastDishId = dish2.id;
					this._currentDish = dish2;
					break;
				}
			}
		}

		// Token: 0x06006188 RID: 24968 RVA: 0x002467A4 File Offset: 0x002449A4
		private Task ObserveChef()
		{
			IEnumerable<CardModel> enumerable = base.Owner.Deck.Cards.Where((CardModel c) => c.IsUpgradable);
			IEnumerable<CardModel> enumerable2 = (enumerable as CardModel[]) ?? enumerable.ToArray<CardModel>();
			if (enumerable2.Any<CardModel>())
			{
				CardCmd.Upgrade(base.Rng.NextItem<CardModel>(enumerable2), CardPreviewStyle.HorizontalLayout);
			}
			base.SetEventFinished(base.L10NLookup("ENDLESS_CONVEYOR.pages.OBSERVE_CHEF.description"));
			return Task.CompletedTask;
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00246827 File Offset: 0x00244A27
		private Task Leave()
		{
			base.SetEventFinished(base.L10NLookup("ENDLESS_CONVEYOR.pages.LEAVE.description"));
			return Task.CompletedTask;
		}

		// Token: 0x0400248E RID: 9358
		private const string _currentDishTitleKey = "CurrentDishTitle";

		// Token: 0x0400248F RID: 9359
		private const string _lastDishTitleKey = "LastDishTitle";

		// Token: 0x04002490 RID: 9360
		private const string _goldenFyshGoldKey = "GoldenFyshGold";

		// Token: 0x04002491 RID: 9361
		private const string _clamRollHealKey = "ClamRollHeal";

		// Token: 0x04002492 RID: 9362
		private const string _caviarMaxHpKey = "CaviarMaxHp";

		// Token: 0x04002493 RID: 9363
		private string _lastDishId = "";

		// Token: 0x04002494 RID: 9364
		private int _numOfGrabs;

		// Token: 0x04002495 RID: 9365
		private EndlessConveyor.Dish _currentDish;

		// Token: 0x02001D29 RID: 7465
		[Nullable(0)]
		private struct Dish : IEquatable<EndlessConveyor.Dish>
		{
			// Token: 0x0600AA75 RID: 43637 RVA: 0x00379134 File Offset: 0x00377334
			public Dish(string id, Func<Task> action, IEnumerable<IHoverTip> hoverTips, float weight)
			{
				this.id = id;
				this.title = new LocString("events", "ENDLESS_CONVEYOR.DISHES." + id + ".title");
				this.optionKey = "ENDLESS_CONVEYOR.pages.ALL.options." + id;
				this.action = action;
				this.hoverTips = hoverTips;
				this.weight = weight;
			}

			// Token: 0x0600AA76 RID: 43638 RVA: 0x00379190 File Offset: 0x00377390
			[NullableContext(0)]
			[CompilerGenerated]
			public override readonly string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Dish");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x0600AA77 RID: 43639 RVA: 0x003791DC File Offset: 0x003773DC
			[NullableContext(0)]
			[CompilerGenerated]
			private readonly bool PrintMembers(StringBuilder builder)
			{
				builder.Append("id = ");
				builder.Append(this.id);
				builder.Append(", title = ");
				builder.Append(this.title);
				builder.Append(", optionKey = ");
				builder.Append(this.optionKey);
				builder.Append(", hoverTips = ");
				builder.Append(this.hoverTips);
				builder.Append(", weight = ");
				builder.Append(this.weight.ToString());
				builder.Append(", action = ");
				builder.Append(this.action);
				return true;
			}

			// Token: 0x0600AA78 RID: 43640 RVA: 0x0037928E File Offset: 0x0037748E
			[CompilerGenerated]
			public static bool operator !=(EndlessConveyor.Dish left, EndlessConveyor.Dish right)
			{
				return !(left == right);
			}

			// Token: 0x0600AA79 RID: 43641 RVA: 0x0037929A File Offset: 0x0037749A
			[CompilerGenerated]
			public static bool operator ==(EndlessConveyor.Dish left, EndlessConveyor.Dish right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600AA7A RID: 43642 RVA: 0x003792A4 File Offset: 0x003774A4
			[CompilerGenerated]
			public override readonly int GetHashCode()
			{
				return ((((EqualityComparer<string>.Default.GetHashCode(this.id) * -1521134295 + EqualityComparer<LocString>.Default.GetHashCode(this.title)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.optionKey)) * -1521134295 + EqualityComparer<IEnumerable<IHoverTip>>.Default.GetHashCode(this.hoverTips)) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.weight)) * -1521134295 + EqualityComparer<Func<Task>>.Default.GetHashCode(this.action);
			}

			// Token: 0x0600AA7B RID: 43643 RVA: 0x00379334 File Offset: 0x00377534
			[NullableContext(0)]
			[CompilerGenerated]
			public override readonly bool Equals(object obj)
			{
				return obj is EndlessConveyor.Dish && this.Equals((EndlessConveyor.Dish)obj);
			}

			// Token: 0x0600AA7C RID: 43644 RVA: 0x0037934C File Offset: 0x0037754C
			[CompilerGenerated]
			public readonly bool Equals(EndlessConveyor.Dish other)
			{
				return EqualityComparer<string>.Default.Equals(this.id, other.id) && EqualityComparer<LocString>.Default.Equals(this.title, other.title) && EqualityComparer<string>.Default.Equals(this.optionKey, other.optionKey) && EqualityComparer<IEnumerable<IHoverTip>>.Default.Equals(this.hoverTips, other.hoverTips) && EqualityComparer<float>.Default.Equals(this.weight, other.weight) && EqualityComparer<Func<Task>>.Default.Equals(this.action, other.action);
			}

			// Token: 0x0400750F RID: 29967
			public readonly string id;

			// Token: 0x04007510 RID: 29968
			public readonly LocString title;

			// Token: 0x04007511 RID: 29969
			public readonly string optionKey;

			// Token: 0x04007512 RID: 29970
			public readonly IEnumerable<IHoverTip> hoverTips;

			// Token: 0x04007513 RID: 29971
			public readonly float weight;

			// Token: 0x04007514 RID: 29972
			public readonly Func<Task> action;
		}
	}
}
