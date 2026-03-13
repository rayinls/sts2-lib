using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200098E RID: 2446
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HiddenDaggers : CardModel
	{
		// Token: 0x06006C17 RID: 27671 RVA: 0x0025E76B File Offset: 0x0025C96B
		public HiddenDaggers()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x06006C18 RID: 27672 RVA: 0x0025E778 File Offset: 0x0025C978
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(2),
					new DynamicVar("Shivs", 2m)
				});
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x06006C19 RID: 27673 RVA: 0x0025E7A1 File Offset: 0x0025C9A1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Shiv>(base.IsUpgraded));
			}
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x0025E7B4 File Offset: 0x0025C9B4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, base.DynamicVars.Cards.IntValue), null, this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			await CardCmd.Discard(choiceContext, enumerable2);
			IEnumerable<CardModel> enumerable3 = await Shiv.CreateInHand(base.Owner, base.DynamicVars["Shivs"].IntValue, base.CombatState);
			if (base.IsUpgraded)
			{
				foreach (CardModel cardModel in enumerable3)
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
			}
		}

		// Token: 0x0400258F RID: 9615
		private const string _shivKey = "Shivs";
	}
}
