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
	// Token: 0x020009F3 RID: 2547
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Parse : CardModel
	{
		// Token: 0x06006E3D RID: 28221 RVA: 0x00262CC0 File Offset: 0x00260EC0
		public Parse()
			: base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x06006E3E RID: 28222 RVA: 0x00262CCD File Offset: 0x00260ECD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001DC3 RID: 7619
		// (get) Token: 0x06006E3F RID: 28223 RVA: 0x00262CDA File Offset: 0x00260EDA
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Ethereal);
			}
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x00262CE4 File Offset: 0x00260EE4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.IntValue, base.Owner, false);
		}

		// Token: 0x06006E41 RID: 28225 RVA: 0x00262D2F File Offset: 0x00260F2F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
