using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200090E RID: 2318
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DecisionsDecisions : CardModel
	{
		// Token: 0x0600695F RID: 26975 RVA: 0x00259333 File Offset: 0x00257533
		public DecisionsDecisions()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x06006960 RID: 26976 RVA: 0x00259340 File Offset: 0x00257540
		public override int CanonicalStarCost
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x06006961 RID: 26977 RVA: 0x00259343 File Offset: 0x00257543
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new RepeatVar(3)
				});
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x06006962 RID: 26978 RVA: 0x00259362 File Offset: 0x00257562
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006963 RID: 26979 RVA: 0x0025936C File Offset: 0x0025756C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.IntValue, base.Owner, false);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1)
			{
				PretendCardsCanBePlayed = true
			};
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, (CardModel c) => c.Type == CardType.Skill && !c.Keywords.Contains(CardKeyword.Unplayable), this);
			CardModel card = enumerable.FirstOrDefault<CardModel>();
			if (card != null)
			{
				for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
				{
					await CardCmd.AutoPlay(choiceContext, card, null, AutoPlayType.Default, false, false);
				}
			}
		}

		// Token: 0x06006964 RID: 26980 RVA: 0x002593B7 File Offset: 0x002575B7
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(2m);
		}
	}
}
