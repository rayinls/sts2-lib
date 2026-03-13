using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A3A RID: 2618
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SecretTechnique : CardModel
	{
		// Token: 0x06006FB3 RID: 28595 RVA: 0x00265BFC File Offset: 0x00263DFC
		public SecretTechnique()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x06006FB4 RID: 28596 RVA: 0x00265C09 File Offset: 0x00263E09
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006FB5 RID: 28597 RVA: 0x00265C14 File Offset: 0x00263E14
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			List<CardModel> list = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.Type == CardType.Skill).ToList<CardModel>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x06006FB6 RID: 28598 RVA: 0x00265C5F File Offset: 0x00263E5F
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
