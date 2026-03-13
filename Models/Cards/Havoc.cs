using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000983 RID: 2435
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Havoc : CardModel
	{
		// Token: 0x06006BE3 RID: 27619 RVA: 0x0025DFCB File Offset: 0x0025C1CB
		public Havoc()
			: base(1, CardType.Skill, CardRarity.Common, TargetType.Self, true)
		{
		}

		// Token: 0x06006BE4 RID: 27620 RVA: 0x0025DFD8 File Offset: 0x0025C1D8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.AutoPlayFromDrawPile(choiceContext, base.Owner, 1, CardPilePosition.Top, true);
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x06006BE5 RID: 27621 RVA: 0x0025E023 File Offset: 0x0025C223
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06006BE6 RID: 27622 RVA: 0x0025E030 File Offset: 0x0025C230
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
