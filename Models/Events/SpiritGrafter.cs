using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Events;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Events
{
	// Token: 0x020007EB RID: 2027
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpiritGrafter : EventModel
	{
		// Token: 0x06006269 RID: 25193 RVA: 0x0024AB34 File Offset: 0x00248D34
		protected override IReadOnlyList<EventOption> GenerateInitialOptions()
		{
			return new <>z__ReadOnlyArray<EventOption>(new EventOption[]
			{
				new EventOption(this, new Func<Task>(this.StepInside), "SPIRIT_GRAFTER.pages.INITIAL.options.LET_IT_IN", HoverTipFactory.FromCardWithCardHoverTips<Metamorphosis>(false)),
				new EventOption(this, new Func<Task>(this.StickArmIn), "SPIRIT_GRAFTER.pages.INITIAL.options.REJECTION", Array.Empty<IHoverTip>()).ThatDoesDamage(base.DynamicVars["RejectionHpLoss"].BaseValue)
			});
		}

		// Token: 0x17001861 RID: 6241
		// (get) Token: 0x0600626A RID: 25194 RVA: 0x0024ABA5 File Offset: 0x00248DA5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new HpLossVar("RejectionHpLoss", 9m),
					new HealVar("LetItInHealAmount", 25m)
				});
			}
		}

		// Token: 0x0600626B RID: 25195 RVA: 0x0024ABDC File Offset: 0x00248DDC
		private async Task StepInside()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars["LetItInHealAmount"].BaseValue, true);
			CardModel cardModel = base.Owner.RunState.CreateCard<Metamorphosis>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardCmd.PreviewCardPileAdd(cardPileAddResult, 1.2f, CardPreviewStyle.HorizontalLayout);
			base.SetEventFinished(base.L10NLookup("SPIRIT_GRAFTER.pages.LET_IT_IN.description"));
		}

		// Token: 0x0600626C RID: 25196 RVA: 0x0024AC20 File Offset: 0x00248E20
		private async Task StickArmIn()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			List<CardModel> list = enumerable.ToList<CardModel>();
			await CardPileCmd.RemoveFromDeck(list, true);
			await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars["RejectionHpLoss"].BaseValue, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
			base.SetEventFinished(base.L10NLookup("SPIRIT_GRAFTER.pages.REJECTION.description"));
		}

		// Token: 0x040024CC RID: 9420
		private const string _rejectionHpLossKey = "RejectionHpLoss";

		// Token: 0x040024CD RID: 9421
		private const string _letItInHealAmountKey = "LetItInHealAmount";
	}
}
