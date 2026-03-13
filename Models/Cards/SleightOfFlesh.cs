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
	// Token: 0x02000A52 RID: 2642
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SleightOfFlesh : CardModel
	{
		// Token: 0x06007035 RID: 28725 RVA: 0x00266B72 File Offset: 0x00264D72
		public SleightOfFlesh()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001E93 RID: 7827
		// (get) Token: 0x06007036 RID: 28726 RVA: 0x00266B7F File Offset: 0x00264D7F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SleightOfFleshPower>(9m));
			}
		}

		// Token: 0x06007037 RID: 28727 RVA: 0x00266B94 File Offset: 0x00264D94
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SleightOfFleshPower>(base.Owner.Creature, base.DynamicVars["SleightOfFleshPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007038 RID: 28728 RVA: 0x00266BD7 File Offset: 0x00264DD7
		protected override void OnUpgrade()
		{
			base.DynamicVars["SleightOfFleshPower"].UpgradeValueBy(4m);
		}
	}
}
