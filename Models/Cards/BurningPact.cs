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

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008CB RID: 2251
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BurningPact : CardModel
	{
		// Token: 0x06006814 RID: 26644 RVA: 0x00256B03 File Offset: 0x00254D03
		public BurningPact()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06006815 RID: 26645 RVA: 0x00256B10 File Offset: 0x00254D10
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06006816 RID: 26646 RVA: 0x00256B1D File Offset: 0x00254D1D
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06006817 RID: 26647 RVA: 0x00256B2C File Offset: 0x00254D2C
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

		// Token: 0x06006818 RID: 26648 RVA: 0x00256B77 File Offset: 0x00254D77
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
