using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedInfernalBlade : CardModel
	{
		public RedInfernalBlade()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006C68 RID: 27752 RVA: 0x0025F108 File Offset: 0x0025D308
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new CardKeyword[] { CardKeyword.Exhaust };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardModel cardModel = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Attack
				select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				cardModel.SetToFreeThisTurn();
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}

		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
