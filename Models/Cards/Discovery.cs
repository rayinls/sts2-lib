using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000920 RID: 2336
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Discovery : CardModel
	{
		// Token: 0x060069C4 RID: 27076 RVA: 0x00259E02 File Offset: 0x00258002
		public Discovery()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x060069C5 RID: 27077 RVA: 0x00259E0F File Offset: 0x0025800F
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060069C6 RID: 27078 RVA: 0x00259E18 File Offset: 0x00258018
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardModel cardModel2;
			if (this._mockSelectedCard == null)
			{
				List<CardModel> list = CardFactory.GetDistinctForCombat(base.Owner, base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint), 3, base.Owner.RunState.Rng.CombatCardGeneration).ToList<CardModel>();
				CardModel cardModel = await CardSelectCmd.FromChooseACardScreen(choiceContext, list, base.Owner, true);
				cardModel2 = cardModel;
			}
			else
			{
				cardModel2 = this._mockSelectedCard;
			}
			if (cardModel2 != null)
			{
				cardModel2.EnergyCost.SetThisTurnOrUntilPlayed(0, false);
				await CardPileCmd.AddGeneratedCardToCombat(cardModel2, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}

		// Token: 0x060069C7 RID: 27079 RVA: 0x00259E63 File Offset: 0x00258063
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}

		// Token: 0x060069C8 RID: 27080 RVA: 0x00259E6C File Offset: 0x0025806C
		public void MockSelectedCard(CardModel card)
		{
			base.AssertMutable();
			this._mockSelectedCard = card;
		}

		// Token: 0x0400256E RID: 9582
		[Nullable(2)]
		private CardModel _mockSelectedCard;
	}
}
