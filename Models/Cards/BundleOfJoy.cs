using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008C9 RID: 2249
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BundleOfJoy : CardModel
	{
		// Token: 0x06006808 RID: 26632 RVA: 0x00256A05 File Offset: 0x00254C05
		public BundleOfJoy()
			: base(2, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06006809 RID: 26633 RVA: 0x00256A12 File Offset: 0x00254C12
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x0600680A RID: 26634 RVA: 0x00256A1F File Offset: 0x00254C1F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x0600680B RID: 26635 RVA: 0x00256A28 File Offset: 0x00254C28
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> distinctForCombat = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.RunState.CardMultiplayerConstraint), base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration);
			foreach (CardModel cardModel in distinctForCombat)
			{
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x0600680C RID: 26636 RVA: 0x00256A6B File Offset: 0x00254C6B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
