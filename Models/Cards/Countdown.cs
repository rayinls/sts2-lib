using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008F6 RID: 2294
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Countdown : CardModel
	{
		// Token: 0x060068E7 RID: 26855 RVA: 0x002584B7 File Offset: 0x002566B7
		public Countdown()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B79 RID: 7033
		// (get) Token: 0x060068E8 RID: 26856 RVA: 0x002584C4 File Offset: 0x002566C4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<CountdownPower>(6m));
			}
		}

		// Token: 0x17001B7A RID: 7034
		// (get) Token: 0x060068E9 RID: 26857 RVA: 0x002584D6 File Offset: 0x002566D6
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x060068EA RID: 26858 RVA: 0x002584E4 File Offset: 0x002566E4
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<CountdownPower>(base.Owner.Creature, base.DynamicVars["CountdownPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x060068EB RID: 26859 RVA: 0x00258527 File Offset: 0x00256727
		protected override void OnUpgrade()
		{
			base.DynamicVars["CountdownPower"].UpgradeValueBy(3m);
		}
	}
}
