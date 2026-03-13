using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A65 RID: 2661
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Splash : CardModel
	{
		// Token: 0x060070A7 RID: 28839 RVA: 0x0026791B File Offset: 0x00265B1B
		public Splash()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x060070A8 RID: 28840 RVA: 0x00267928 File Offset: 0x00265B28
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardModel cardModel3;
			if (this._mockGeneratedCard == null)
			{
				List<CardPoolModel> list = base.Owner.UnlockState.CharacterCardPools.ToList<CardPoolModel>();
				if (list.Count > 1)
				{
					list.Remove(base.Owner.Character.CardPool);
				}
				IEnumerable<CardModel> enumerable = from c in list.SelectMany((CardPoolModel c) => c.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint))
					where c.Type == CardType.Attack
					select c;
				List<CardModel> list2 = CardFactory.GetDistinctForCombat(base.Owner, enumerable, 3, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
				if (base.IsUpgraded)
				{
					foreach (CardModel cardModel in list2)
					{
						CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
					}
				}
				CardModel cardModel2 = await CardSelectCmd.FromChooseACardScreen(choiceContext, list2, base.Owner, true);
				cardModel3 = cardModel2;
			}
			else
			{
				cardModel3 = this._mockGeneratedCard;
				if (base.IsUpgraded)
				{
					CardCmd.Upgrade(cardModel3, CardPreviewStyle.HorizontalLayout);
				}
			}
			if (cardModel3 != null)
			{
				cardModel3.SetToFreeThisTurn();
				await CardPileCmd.AddGeneratedCardToCombat(cardModel3, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}

		// Token: 0x060070A9 RID: 28841 RVA: 0x00267973 File Offset: 0x00265B73
		public void MockGeneratedCard(CardModel card)
		{
			base.AssertMutable();
			this._mockGeneratedCard = card;
		}

		// Token: 0x040025D4 RID: 9684
		[Nullable(2)]
		private CardModel _mockGeneratedCard;
	}
}
