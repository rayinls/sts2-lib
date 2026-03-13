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
	// Token: 0x02000A56 RID: 2646
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Smokestack : CardModel
	{
		// Token: 0x06007047 RID: 28743 RVA: 0x00266D57 File Offset: 0x00264F57
		public Smokestack()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x06007048 RID: 28744 RVA: 0x00266D64 File Offset: 0x00264F64
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SmokestackPower>(5m));
			}
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x00266D78 File Offset: 0x00264F78
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SmokestackPower>(base.Owner.Creature, base.DynamicVars["SmokestackPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600704A RID: 28746 RVA: 0x00266DBB File Offset: 0x00264FBB
		protected override void OnUpgrade()
		{
			base.DynamicVars["SmokestackPower"].UpgradeValueBy(2m);
		}
	}
}
