using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A7B RID: 2683
	public sealed class Subroutine : CardModel
	{
		// Token: 0x0600711C RID: 28956 RVA: 0x002688CF File Offset: 0x00266ACF
		public Subroutine()
			: base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self, true)
		{
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x002688DC File Offset: 0x00266ADC
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<SubroutinePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x0026891F File Offset: 0x00266B1F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
