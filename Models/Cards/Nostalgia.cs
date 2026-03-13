using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020009E2 RID: 2530
	public sealed class Nostalgia : CardModel
	{
		// Token: 0x06006DEB RID: 28139 RVA: 0x002622BF File Offset: 0x002604BF
		public Nostalgia()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x06006DEC RID: 28140 RVA: 0x002622CC File Offset: 0x002604CC
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<NostalgiaPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x06006DED RID: 28141 RVA: 0x0026230F File Offset: 0x0026050F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
