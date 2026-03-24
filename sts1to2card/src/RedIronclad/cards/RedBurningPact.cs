using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedBurningPact : CardModel
	{
		public RedBurningPact()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// (get) Token: 0x06006815 RID: 26645 RVA: 0x00256B10 File Offset: 0x00254D10
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new DynamicVar[] { new CardsVar(2) };
			}
		}

		// (get) Token: 0x06006816 RID: 26646 RVA: 0x00256B1D File Offset: 0x00254D1D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromKeyword(CardKeyword.Exhaust) };
			}
		}

		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.ExhaustSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
