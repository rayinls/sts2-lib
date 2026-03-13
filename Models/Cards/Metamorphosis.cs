using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009C7 RID: 2503
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Metamorphosis : CardModel
	{
		// Token: 0x06006D54 RID: 27988 RVA: 0x00261064 File Offset: 0x0025F264
		public Metamorphosis()
			: base(2, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06006D55 RID: 27989 RVA: 0x00261071 File Offset: 0x0025F271
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x06006D56 RID: 27990 RVA: 0x00261079 File Offset: 0x0025F279
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x00261088 File Offset: 0x0025F288
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> forCombat = CardFactory.GetForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Attack
				select c, base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration);
			foreach (CardModel cardModel in forCombat)
			{
				cardModel.SetToFreeThisCombat();
				CardPileAddResult cardPileAddResult = await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Draw, true, CardPilePosition.Random);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x06006D58 RID: 27992 RVA: 0x002610CB File Offset: 0x0025F2CB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(2m);
		}
	}
}
