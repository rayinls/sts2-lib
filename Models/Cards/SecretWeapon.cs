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
	// Token: 0x02000A3B RID: 2619
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SecretWeapon : CardModel
	{
		// Token: 0x06006FB7 RID: 28599 RVA: 0x00265C68 File Offset: 0x00263E68
		public SecretWeapon()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x06006FB8 RID: 28600 RVA: 0x00265C75 File Offset: 0x00263E75
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006FB9 RID: 28601 RVA: 0x00265C80 File Offset: 0x00263E80
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			List<CardModel> list = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.Type == CardType.Attack).ToList<CardModel>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x06006FBA RID: 28602 RVA: 0x00265CCB File Offset: 0x00263ECB
		protected override void OnUpgrade()
		{
			base.RemoveKeyword(CardKeyword.Exhaust);
		}
	}
}
