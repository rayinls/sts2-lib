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
	// Token: 0x02000ABD RID: 2749
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WhiteNoise : CardModel
	{
		// Token: 0x06007275 RID: 29301 RVA: 0x0026B18C File Offset: 0x0026938C
		public WhiteNoise()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001F7A RID: 8058
		// (get) Token: 0x06007276 RID: 29302 RVA: 0x0026B199 File Offset: 0x00269399
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06007277 RID: 29303 RVA: 0x0026B1A4 File Offset: 0x002693A4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			CardModel cardModel = CardFactory.GetDistinctForCombat(base.Owner, from c in base.Owner.Character.CardPool.GetUnlockedCards(base.Owner.UnlockState, base.Owner.RunState.CardMultiplayerConstraint)
				where c.Type == CardType.Power
				select c, 1, base.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				cardModel.SetToFreeThisTurn();
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}

		// Token: 0x06007278 RID: 29304 RVA: 0x0026B1E7 File Offset: 0x002693E7
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
