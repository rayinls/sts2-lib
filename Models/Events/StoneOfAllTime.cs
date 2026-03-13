using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007EC RID: 2028
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StoneOfAllTime : EventModel
	{
		// Token: 0x17001862 RID: 6242
		// (get) Token: 0x0600626E RID: 25198 RVA: 0x0024AC6B File Offset: 0x00248E6B
		// (set) Token: 0x0600626F RID: 25199 RVA: 0x0024AC73 File Offset: 0x00248E73
		[Nullable(2)]
		private PotionModel DrinkAndLiftPotion
		{
			[NullableContext(2)]
			get
			{
				return this._drinkAndLiftPotion;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._drinkAndLiftPotion = value;
			}
		}

		// Token: 0x17001863 RID: 6243
		// (get) Token: 0x06006270 RID: 25200 RVA: 0x0024AC84 File Offset: 0x00248E84
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new StringVar("DrinkRandomPotion", ""),
					new DynamicVar("DrinkMaxHpGain", 10m),
					new DynamicVar("PushHpLoss", 6m),
					new DynamicVar("PushVigorousAmount", 8m)
				});
			}
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x0024ACE8 File Offset: 0x00248EE8
		public override bool IsAllowed(RunState runState)
		{
			if (runState.CurrentActIndex == 1)
			{
				return runState.Players.All((Player player) => player.Potions.Any<PotionModel>());
			}
			return false;
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0024AD1F File Offset: 0x00248F1F
		protected override Task BeforeEventStarted()
		{
			base.Owner.CanRemovePotions = false;
			return Task.CompletedTask;
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x0024AD32 File Offset: 0x00248F32
		protected override void OnEventFinished()
		{
			base.Owner.CanRemovePotions = true;
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0024AD40 File Offset: 0x00248F40
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			this.DrinkAndLiftPotion = base.Rng.NextItem<PotionModel>(base.Owner.Potions);
			EventOption eventOption;
			if (this.DrinkAndLiftPotion != null)
			{
				StringVar stringVar = (StringVar)base.DynamicVars["DrinkRandomPotion"];
				stringVar.StringValue = this.DrinkAndLiftPotion.Title.GetFormattedText();
				eventOption = new EventOption(this, new Func<Task>(this.Lift), "STONE_OF_ALL_TIME.pages.INITIAL.options.LIFT", new IHoverTip[] { HoverTipFactory.FromPotion(this.DrinkAndLiftPotion) });
			}
			else
			{
				eventOption = new EventOption(this, null, "STONE_OF_ALL_TIME.pages.INITIAL.options.LIFT_LOCKED", Array.Empty<IHoverTip>());
			}
			EventOption eventOption2;
			if (CardPile.Get(PileType.Deck, base.Owner).Cards.Count((CardModel c) => ModelDb.Enchantment<Vigorous>().CanEnchant(c)) >= 1)
			{
				eventOption2 = new EventOption(this, new Func<Task>(this.Push), "STONE_OF_ALL_TIME.pages.INITIAL.options.PUSH", HoverTipFactory.FromEnchantment<Vigorous>(base.DynamicVars["PushVigorousAmount"].IntValue)).ThatDoesDamage(base.DynamicVars["PushHpLoss"].BaseValue);
			}
			else
			{
				eventOption2 = new EventOption(this, null, "STONE_OF_ALL_TIME.pages.INITIAL.options.PUSH_LOCKED", Array.Empty<IHoverTip>());
			}
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[] { eventOption, eventOption2 });
		}

		// Token: 0x06006275 RID: 25205 RVA: 0x0024AE88 File Offset: 0x00249088
		private async Task Lift()
		{
			await PotionCmd.Discard(this.DrinkAndLiftPotion);
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars["DrinkMaxHpGain"].BaseValue);
			base.Rng.NextInt(100);
			LocString locString = base.L10NLookup("STONE_OF_ALL_TIME.pages.LIFT.description");
			locString.Add(base.DynamicVars["DrinkRandomPotion"]);
			base.SetEventFinished(locString);
		}

		// Token: 0x06006276 RID: 25206 RVA: 0x0024AECC File Offset: 0x002490CC
		private async Task Push()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars["PushHpLoss"].BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			Vigorous vigorous = ModelDb.Enchantment<Vigorous>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, vigorous, base.DynamicVars["PushVigorousAmount"].IntValue, cardSelectorPrefs);
			foreach (CardModel cardModel in enumerable)
			{
				CardCmd.Enchant(vigorous.ToMutable(), cardModel, base.DynamicVars["PushVigorousAmount"].BaseValue);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
			base.Rng.NextInt(100);
			base.SetEventFinished(base.L10NLookup("STONE_OF_ALL_TIME.pages.PUSH.description"));
		}

		// Token: 0x040024CE RID: 9422
		private const string _drinkRandomPotionKey = "DrinkRandomPotion";

		// Token: 0x040024CF RID: 9423
		private const string _drinkMaxHpGain = "DrinkMaxHpGain";

		// Token: 0x040024D0 RID: 9424
		private const string _pushHpLoss = "PushHpLoss";

		// Token: 0x040024D1 RID: 9425
		private const string _pushVigorousAmountKey = "PushVigorousAmount";

		// Token: 0x040024D2 RID: 9426
		[Nullable(2)]
		private PotionModel _drinkAndLiftPotion;
	}
}
