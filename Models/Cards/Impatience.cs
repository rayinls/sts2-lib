using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000999 RID: 2457
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Impatience : CardModel
	{
		// Token: 0x06006C55 RID: 27733 RVA: 0x0025EF38 File Offset: 0x0025D138
		public Impatience()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x06006C56 RID: 27734 RVA: 0x0025EF45 File Offset: 0x0025D145
		protected override bool ShouldGlowGoldInternal
		{
			get
			{
				return PileType.Hand.GetPile(base.Owner).Cards.All((CardModel c) => c.Type != CardType.Attack);
			}
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x0025EF7C File Offset: 0x0025D17C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06006C58 RID: 27736 RVA: 0x0025EF89 File Offset: 0x0025D189
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x0025EFA0 File Offset: 0x0025D1A0
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (!PileType.Hand.GetPile(base.Owner).Cards.Any((CardModel c) => c.Type == CardType.Attack))
			{
				await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
			}
		}
	}
}
