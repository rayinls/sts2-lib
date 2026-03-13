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
	// Token: 0x020008D2 RID: 2258
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CalculatedGamble : CardModel
	{
		// Token: 0x06006830 RID: 26672 RVA: 0x00256E3C File Offset: 0x0025503C
		public CalculatedGamble()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B2D RID: 6957
		// (get) Token: 0x06006831 RID: 26673 RVA: 0x00256E49 File Offset: 0x00255049
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x00256E54 File Offset: 0x00255054
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			IEnumerable<CardModel> cards = PileType.Hand.GetPile(base.Owner).Cards;
			int num = cards.Count<CardModel>();
			await CardCmd.DiscardAndDraw(choiceContext, cards, num);
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x00256E9F File Offset: 0x0025509F
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
