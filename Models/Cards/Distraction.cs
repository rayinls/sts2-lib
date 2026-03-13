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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000923 RID: 2339
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Distraction : CardModel
	{
		// Token: 0x060069D4 RID: 27092 RVA: 0x00259FB3 File Offset: 0x002581B3
		public Distraction()
			: base(1, CardType.Skill, CardRarity.Event, TargetType.Self, true)
		{
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x060069D5 RID: 27093 RVA: 0x00259FC0 File Offset: 0x002581C0
		public override CardPoolModel VisualCardPool
		{
			get
			{
				return ModelDb.CardPool<SilentCardPool>();
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x060069D6 RID: 27094 RVA: 0x00259FC7 File Offset: 0x002581C7
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060069D7 RID: 27095 RVA: 0x00259FD0 File Offset: 0x002581D0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardModel cardModel = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Skill
				select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				cardModel.SetToFreeThisTurn();
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}

		// Token: 0x060069D8 RID: 27096 RVA: 0x0025A013 File Offset: 0x00258213
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
