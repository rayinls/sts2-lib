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
	// Token: 0x02000A22 RID: 2594
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Reflex : CardModel
	{
		// Token: 0x06006F2D RID: 28461 RVA: 0x00264BB3 File Offset: 0x00262DB3
		public Reflex()
			: base(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E21 RID: 7713
		// (get) Token: 0x06006F2E RID: 28462 RVA: 0x00264BC0 File Offset: 0x00262DC0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x17001E22 RID: 7714
		// (get) Token: 0x06006F2F RID: 28463 RVA: 0x00264BCD File Offset: 0x00262DCD
		public override IEnumerable<CardKeyword> CanonicalKeywords
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<CardKeyword>(CardKeyword.Sly);
			}
		}

		// Token: 0x06006F30 RID: 28464 RVA: 0x00264BD8 File Offset: 0x00262DD8
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006F31 RID: 28465 RVA: 0x00264C23 File Offset: 0x00262E23
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
