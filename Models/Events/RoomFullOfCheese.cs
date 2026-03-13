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
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007E5 RID: 2021
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RoomFullOfCheese : EventModel
	{
		// Token: 0x06006237 RID: 25143 RVA: 0x00249F2B File Offset: 0x0024812B
		public override bool IsAllowed(RunState runState)
		{
			return runState.CurrentActIndex < 2;
		}

		// Token: 0x17001858 RID: 6232
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x00249F36 File Offset: 0x00248136
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(14m, ValueProp.Unblockable | ValueProp.Unpowered));
			}
		}

		// Token: 0x06006239 RID: 25145 RVA: 0x00249F4C File Offset: 0x0024814C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.Gorge), "ROOM_FULL_OF_CHEESE.pages.INITIAL.options.GORGE", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Search), "ROOM_FULL_OF_CHEESE.pages.INITIAL.options.SEARCH", HoverTipFactory.FromRelic<ChosenCheese>()).ThatDoesDamage(base.DynamicVars.Damage.BaseValue)
			});
		}

		// Token: 0x0600623A RID: 25146 RVA: 0x00249FB8 File Offset: 0x002481B8
		private async Task Gorge()
		{
			Player owner = base.Owner;
			CardCreationOptions cardCreationOptions = CardCreationOptions.ForNonCombatWithUniformOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(owner.Character.CardPool), (CardModel c) => c.Rarity == CardRarity.Common).WithFlags(CardCreationFlags.NoRarityModification);
			List<CardCreationResult> list = CardFactory.CreateForReward(owner, 8, cardCreationOptions).ToList<CardCreationResult>();
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.L10NLookup("ROOM_FULL_OF_CHEESE.pages.GORGE.selectionScreenPrompt"), 2);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGridForRewards(new BlockingPlayerChoiceContext(), list, owner, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false), 1.2f, CardPreviewStyle.HorizontalLayout);
			}
			IEnumerator<CardModel> enumerator = null;
			base.SetEventFinished(base.L10NLookup("ROOM_FULL_OF_CHEESE.pages.GORGE.description"));
		}

		// Token: 0x0600623B RID: 25147 RVA: 0x00249FFC File Offset: 0x002481FC
		private async Task Search()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars.Damage, null, null);
			await RelicCmd.Obtain<ChosenCheese>(base.Owner);
			base.SetEventFinished(base.L10NLookup("ROOM_FULL_OF_CHEESE.pages.SEARCH.description"));
		}
	}
}
