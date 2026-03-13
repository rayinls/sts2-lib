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
	// Token: 0x02000A61 RID: 2657
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Speedster : CardModel
	{
		// Token: 0x06007093 RID: 28819 RVA: 0x00267669 File Offset: 0x00265869
		public Speedster()
			: base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x06007094 RID: 28820 RVA: 0x00267676 File Offset: 0x00265876
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<SpeedsterPower>(2m));
			}
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x00267688 File Offset: 0x00265888
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<SpeedsterPower>(base.Owner.Creature, base.DynamicVars["SpeedsterPower"].IntValue, base.Owner.Creature, this, false);
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x002676CB File Offset: 0x002658CB
		protected override void OnUpgrade()
		{
			base.DynamicVars["SpeedsterPower"].UpgradeValueBy(1m);
		}
	}
}
