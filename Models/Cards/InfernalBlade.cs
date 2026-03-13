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
	// Token: 0x0200099C RID: 2460
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class InfernalBlade : CardModel
	{
		// Token: 0x06006C67 RID: 27751 RVA: 0x0025F0FB File Offset: 0x0025D2FB
		public InfernalBlade()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x06006C68 RID: 27752 RVA: 0x0025F108 File Offset: 0x0025D308
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006C69 RID: 27753 RVA: 0x0025F110 File Offset: 0x0025D310
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

		// Token: 0x06006C6A RID: 27754 RVA: 0x0025F153 File Offset: 0x0025D353
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
