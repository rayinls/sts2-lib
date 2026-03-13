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
	// Token: 0x020008D5 RID: 2261
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Capacitor : CardModel
	{
		// Token: 0x0600683F RID: 26687 RVA: 0x00256FB0 File Offset: 0x002551B0
		public Capacitor()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001B33 RID: 6963
		// (get) Token: 0x06006840 RID: 26688 RVA: 0x00256FBD File Offset: 0x002551BD
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(2));
			}
		}

		// Token: 0x06006841 RID: 26689 RVA: 0x00256FCC File Offset: 0x002551CC
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await OrbCmd.AddSlots(base.Owner, base.DynamicVars.Repeat.IntValue);
		}

		// Token: 0x06006842 RID: 26690 RVA: 0x0025700F File Offset: 0x0025520F
		protected override void OnUpgrade()
		{
			base.DynamicVars.Repeat.UpgradeValueBy(1m);
		}
	}
}
