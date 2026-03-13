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
	// Token: 0x02000A0A RID: 2570
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Prophesize : CardModel
	{
		// Token: 0x06006EB0 RID: 28336 RVA: 0x00263B74 File Offset: 0x00261D74
		public Prophesize()
			: base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001DEE RID: 7662
		// (get) Token: 0x06006EB1 RID: 28337 RVA: 0x00263B81 File Offset: 0x00261D81
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(6));
			}
		}

		// Token: 0x06006EB2 RID: 28338 RVA: 0x00263B90 File Offset: 0x00261D90
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
			await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner, false);
		}

		// Token: 0x06006EB3 RID: 28339 RVA: 0x00263BDB File Offset: 0x00261DDB
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(3m);
		}
	}
}
