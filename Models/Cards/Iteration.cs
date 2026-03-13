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
	// Token: 0x020009A4 RID: 2468
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Iteration : CardModel
	{
		// Token: 0x06006C8D RID: 27789 RVA: 0x0025F555 File Offset: 0x0025D755
		public Iteration()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x06006C8E RID: 27790 RVA: 0x0025F562 File Offset: 0x0025D762
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<IterationPower>(2m));
			}
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x0025F574 File Offset: 0x0025D774
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<IterationPower>(base.Owner.Creature, base.DynamicVars["IterationPower"].BaseValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x0025F5B7 File Offset: 0x0025D7B7
		protected override void OnUpgrade()
		{
			base.DynamicVars["IterationPower"].UpgradeValueBy(1m);
		}
	}
}
