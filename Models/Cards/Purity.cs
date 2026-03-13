using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A0F RID: 2575
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Purity : CardModel
	{
		// Token: 0x06006ECA RID: 28362 RVA: 0x00263F97 File Offset: 0x00262197
		public Purity()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x06006ECB RID: 28363 RVA: 0x00263FA4 File Offset: 0x002621A4
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlyArray<CardKeyword>(new CardKeyword[]
				{
					CardKeyword.Retain,
					CardKeyword.Exhaust
				});
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x06006ECC RID: 28364 RVA: 0x00263FB9 File Offset: 0x002621B9
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06006ECD RID: 28365 RVA: 0x00263FC8 File Offset: 0x002621C8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 0, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				await CardCmd.Exhaust(choiceContext, cardModel, false, false);
			}
			IEnumerator<CardModel> enumerator = null;
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x00264013 File Offset: 0x00262213
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(2m);
		}
	}
}
