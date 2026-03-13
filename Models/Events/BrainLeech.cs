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
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007C5 RID: 1989
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BrainLeech : EventModel
	{
		// Token: 0x06006119 RID: 24857 RVA: 0x00244711 File Offset: 0x00242911
		public override bool IsAllowed(RunState runState)
		{
			return runState.CurrentActIndex < 2;
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x0024471C File Offset: 0x0024291C
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.ShareKnowledge), "BRAIN_LEECH.pages.INITIAL.options.SHARE_KNOWLEDGE", Array.Empty<IHoverTip>()),
				new EventOption(this, new Func<Task>(this.Rip), "BRAIN_LEECH.pages.INITIAL.options.RIP", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars["RipHpLoss"].BaseValue)
			});
		}

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x0600611B RID: 24859 RVA: 0x0024478C File Offset: 0x0024298C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar("RipHpLoss", 5m, ValueProp.Unblockable | ValueProp.Unpowered),
					new IntVar("RewardCount", 1m),
					new IntVar("CardChoiceCount", 1m),
					new IntVar("FromCardChoiceCount", 5m)
				});
			}
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x002447F0 File Offset: 0x002429F0
		private async Task Rip()
		{
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, (DamageVar)base.DynamicVars["RipHpLoss"], null, null);
			for (int i = 0; i < base.DynamicVars["RewardCount"].IntValue; i++)
			{
				CardReward cardReward = new CardReward(CardCreationOptions.ForNonCombatWithDefaultOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(ModelDb.CardPool<ColorlessCardPool>()), null), 3, base.Owner);
				await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1) { cardReward });
			}
			base.SetEventFinished(base.L10NLookup("BRAIN_LEECH.pages.RIP.description"));
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x00244834 File Offset: 0x00242A34
		private async Task ShareKnowledge()
		{
			Player owner = base.Owner;
			List<CardCreationResult> list = CardFactory.CreateForReward(owner, base.DynamicVars["FromCardChoiceCount"].IntValue, CardCreationOptions.ForNonCombatWithDefaultOdds(new <>z__ReadOnlySingleElementList<CardPoolModel>(owner.Character.CardPool), null)).ToList<CardCreationResult>();
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.L10NLookup("BRAIN_LEECH.pages.SHARE_KNOWLEDGE.selectionScreenPrompt"), 1)
			{
				Cancelable = false
			};
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGridForRewards(new BlockingPlayerChoiceContext(), list, base.Owner, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false), 1.2f, CardPreviewStyle.HorizontalLayout);
			}
			base.SetEventFinished(base.L10NLookup("BRAIN_LEECH.pages.SHARE_KNOWLEDGE.description"));
		}

		// Token: 0x04002470 RID: 9328
		private const string _ripHpLossKey = "RipHpLoss";

		// Token: 0x04002471 RID: 9329
		private const string _rewardCountKey = "RewardCount";

		// Token: 0x04002472 RID: 9330
		private const string _cardChoiceCountKey = "CardChoiceCount";

		// Token: 0x04002473 RID: 9331
		private const string _fromCardChoiceCountKey = "FromCardChoiceCount";
	}
}
