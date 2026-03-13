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
	// Token: 0x02000ABE RID: 2750
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Wish : CardModel
	{
		// Token: 0x06007279 RID: 29305 RVA: 0x0026B1F5 File Offset: 0x002693F5
		public Wish()
			: base(0, CardType.Skill, CardRarity.Ancient, TargetType.Self, true)
		{
		}

		// Token: 0x17001F7B RID: 8059
		// (get) Token: 0x0600727A RID: 29306 RVA: 0x0026B202 File Offset: 0x00269402
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x0600727B RID: 29307 RVA: 0x0026B20C File Offset: 0x0026940C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			List<CardModel> list = (from c in PileType.Draw.GetPile(base.Owner).Cards
				orderby c.Rarity, c.Id
				select c).ToList<CardModel>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromSimpleGrid(choiceContext, list, base.Owner, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			CardModel cardModel = enumerable2.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				await CardPileCmd.Add(cardModel, PileType.Hand, CardPilePosition.Bottom, null, false);
			}
		}

		// Token: 0x0600727C RID: 29308 RVA: 0x0026B257 File Offset: 0x00269457
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
