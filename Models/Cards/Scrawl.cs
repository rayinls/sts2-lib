using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A36 RID: 2614
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Scrawl : CardModel
	{
		// Token: 0x06006F9D RID: 28573 RVA: 0x00265988 File Offset: 0x00263B88
		public Scrawl()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x06006F9E RID: 28574 RVA: 0x00265995 File Offset: 0x00263B95
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006F9F RID: 28575 RVA: 0x002659A0 File Offset: 0x00263BA0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			int num = 10 - base.Owner.PlayerCombatState.Hand.Cards.Count;
			await CardPileCmd.Draw(choiceContext, num, base.Owner, false);
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x002659EB File Offset: 0x00263BEB
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
