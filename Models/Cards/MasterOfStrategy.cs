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
	// Token: 0x020009C1 RID: 2497
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MasterOfStrategy : CardModel
	{
		// Token: 0x06006D35 RID: 27957 RVA: 0x00260BF6 File Offset: 0x0025EDF6
		public MasterOfStrategy()
			: base(0, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001D53 RID: 7507
		// (get) Token: 0x06006D36 RID: 27958 RVA: 0x00260C03 File Offset: 0x0025EE03
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17001D54 RID: 7508
		// (get) Token: 0x06006D37 RID: 27959 RVA: 0x00260C10 File Offset: 0x0025EE10
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006D38 RID: 27960 RVA: 0x00260C18 File Offset: 0x0025EE18
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}

		// Token: 0x06006D39 RID: 27961 RVA: 0x00260C30 File Offset: 0x0025EE30
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}
	}
}
