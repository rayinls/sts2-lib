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
	// Token: 0x0200096E RID: 2414
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glimmer : CardModel
	{
		// Token: 0x06006B78 RID: 27512 RVA: 0x0025D2B3 File Offset: 0x0025B4B3
		public Glimmer()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x06006B79 RID: 27513 RVA: 0x0025D2C0 File Offset: 0x0025B4C0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new DynamicVar("PutBack", 1m)
				});
			}
		}

		// Token: 0x06006B7A RID: 27514 RVA: 0x0025D2E8 File Offset: 0x0025B4E8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, base.DynamicVars["PutBack"].IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHand(choiceContext, base.Owner, cardSelectorPrefs, null, this);
			CardModel[] array = enumerable.ToArray<CardModel>();
			if (array.Length != 0)
			{
				await CardPileCmd.Add(array, PileType.Draw, CardPilePosition.Top, null, false);
			}
		}

		// Token: 0x06006B7B RID: 27515 RVA: 0x0025D333 File Offset: 0x0025B533
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x04002586 RID: 9606
		private const string _putBackKey = "PutBack";
	}
}
