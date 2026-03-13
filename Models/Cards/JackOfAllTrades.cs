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
using MegaCrit.Sts2.Core.Models.CardPools;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009A5 RID: 2469
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JackOfAllTrades : CardModel
	{
		// Token: 0x06006C91 RID: 27793 RVA: 0x0025F5D3 File Offset: 0x0025D7D3
		public JackOfAllTrades()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x0025F5E0 File Offset: 0x0025D7E0
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x06006C93 RID: 27795 RVA: 0x0025F5F7 File Offset: 0x0025D7F7
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x06006C94 RID: 27796 RVA: 0x0025F5FF File Offset: 0x0025D7FF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x0025F60C File Offset: 0x0025D80C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> distinctForCombat = CardFactory.GetDistinctForCombat(base.Owner, from c in ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where !(c is JackOfAllTrades)
				select c, base.DynamicVars.Cards.IntValue, base.Owner.RunState.Rng.CombatCardGeneration);
			foreach (CardModel cardModel in distinctForCombat.ToList<CardModel>())
			{
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
			List<CardModel>.Enumerator enumerator = default(List<CardModel>.Enumerator);
		}
	}
}
