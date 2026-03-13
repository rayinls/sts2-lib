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
	// Token: 0x02000A2E RID: 2606
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RoyalGamble : CardModel
	{
		// Token: 0x06006F74 RID: 28532 RVA: 0x002654EC File Offset: 0x002636EC
		public RoyalGamble()
			: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x06006F75 RID: 28533 RVA: 0x002654F9 File Offset: 0x002636F9
		public override int CanonicalStarCost
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x06006F76 RID: 28534 RVA: 0x002654FC File Offset: 0x002636FC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StarsVar(9));
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x06006F77 RID: 28535 RVA: 0x0026550A File Offset: 0x0026370A
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Exhaust);
			}
		}

		// Token: 0x06006F78 RID: 28536 RVA: 0x00265514 File Offset: 0x00263714
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x00265557 File Offset: 0x00263757
		protected override void OnUpgrade()
		{
			base.AddKeyword(CardKeyword.Retain);
		}
	}
}
