using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A13 RID: 2579
	public sealed class Quasar : CardModel
	{
		// Token: 0x06006EE0 RID: 28384 RVA: 0x00264219 File Offset: 0x00262419
		public Quasar()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06006EE1 RID: 28385 RVA: 0x00264226 File Offset: 0x00262426
		public override int CanonicalStarCost
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x0026422C File Offset: 0x0026242C
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, ModelDb.CardPool<ColorlessCardPool>().GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), 3, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
			if (base.IsUpgraded)
			{
				CardCmd.Upgrade(list, CardPreviewStyle.HorizontalLayout);
			}
			CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, list, base.Owner, true);
			CardModel cardModel2 = cardModel;
			if (cardModel2 != null)
			{
				await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}
	}
}
