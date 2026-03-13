using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A51 RID: 2641
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Skim : CardModel
	{
		// Token: 0x06007031 RID: 28721 RVA: 0x00266AF3 File Offset: 0x00264CF3
		public Skim()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E92 RID: 7826
		// (get) Token: 0x06007032 RID: 28722 RVA: 0x00266B00 File Offset: 0x00264D00
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x06007033 RID: 28723 RVA: 0x00266B10 File Offset: 0x00264D10
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x00266B5B File Offset: 0x00264D5B
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
