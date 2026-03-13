using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x0200095E RID: 2398
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ForegoneConclusion : CardModel
	{
		// Token: 0x06006B18 RID: 27416 RVA: 0x0025C6F1 File Offset: 0x0025A8F1
		public ForegoneConclusion()
			: base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x17001C74 RID: 7284
		// (get) Token: 0x06006B19 RID: 27417 RVA: 0x0025C6FE File Offset: 0x0025A8FE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x0025C70C File Offset: 0x0025A90C
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ForegoneConclusionPower>(base.Owner.Creature, base.DynamicVars.Cards.BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x0025C74F File Offset: 0x0025A94F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Cards.UpgradeValueBy(1m);
		}
	}
}
