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
	// Token: 0x020008D1 RID: 2257
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Calcify : CardModel
	{
		// Token: 0x0600682C RID: 26668 RVA: 0x00256DBD File Offset: 0x00254FBD
		public Calcify()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B2C RID: 6956
		// (get) Token: 0x0600682D RID: 26669 RVA: 0x00256DCA File Offset: 0x00254FCA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<CalcifyPower>(4m));
			}
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x00256DDC File Offset: 0x00254FDC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CalcifyPower>(base.Owner.Creature, base.DynamicVars["CalcifyPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x00256E1F File Offset: 0x0025501F
		protected override void OnUpgrade()
		{
			base.DynamicVars["CalcifyPower"].UpgradeValueBy(2m);
		}
	}
}
