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
	// Token: 0x0200099D RID: 2461
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Inferno : CardModel
	{
		// Token: 0x06006C6B RID: 27755 RVA: 0x0025F161 File Offset: 0x0025D361
		public Inferno()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x06006C6C RID: 27756 RVA: 0x0025F16E File Offset: 0x0025D36E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<InfernoPower>(6m));
			}
		}

		// Token: 0x06006C6D RID: 27757 RVA: 0x0025F180 File Offset: 0x0025D380
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			InfernoPower infernoPower = await PowerCmd.Apply<InfernoPower>(base.Owner.Creature, base.DynamicVars["InfernoPower"].BaseValue, base.Owner.Creature, this, false);
			if (infernoPower != null)
			{
				infernoPower.IncrementSelfDamage();
			}
		}

		// Token: 0x06006C6E RID: 27758 RVA: 0x0025F1C3 File Offset: 0x0025D3C3
		protected override void OnUpgrade()
		{
			base.DynamicVars["InfernoPower"].UpgradeValueBy(3m);
		}
	}
}
