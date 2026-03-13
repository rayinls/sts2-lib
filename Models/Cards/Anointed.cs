using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000891 RID: 2193
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Anointed : CardModel
	{
		// Token: 0x060066E6 RID: 26342 RVA: 0x002544C3 File Offset: 0x002526C3
		public Anointed()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x060066E7 RID: 26343 RVA: 0x002544D0 File Offset: 0x002526D0
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x002544D8 File Offset: 0x002526D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> enumerable = PileType.Draw.GetPile(base.Owner).Cards.Where((CardModel c) => c.Rarity == CardRarity.Rare).ToList<CardModel>();
			await CardPileCmd.Add(enumerable, PileType.Hand, CardPilePosition.Bottom, null, false);
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x0025451B File Offset: 0x0025271B
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
