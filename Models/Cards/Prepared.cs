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
	// Token: 0x02000A05 RID: 2565
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Prepared : CardModel
	{
		// Token: 0x06006E99 RID: 28313 RVA: 0x00263913 File Offset: 0x00261B13
		public Prepared()
			: base(0, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x17001DE5 RID: 7653
		// (get) Token: 0x06006E9A RID: 28314 RVA: 0x00263920 File Offset: 0x00261B20
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06006E9B RID: 28315 RVA: 0x00263930 File Offset: 0x00261B30
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			int cardCount = base.DynamicVars.Cards.IntValue;
			await CardPileCmd.Draw(choiceContext, cardCount, base.Owner, false);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromHandForDiscard(choiceContext, base.Owner, new CardSelectorPrefs(CardSelectorPrefs.DiscardSelectionPrompt, cardCount), null, this);
			await CardCmd.Discard(choiceContext, enumerable);
		}

		// Token: 0x06006E9C RID: 28316 RVA: 0x0026397B File Offset: 0x00261B7B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
