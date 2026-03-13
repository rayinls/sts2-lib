using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x020008D0 RID: 2256
	public sealed class Calamity : CardModel
	{
		// Token: 0x06006829 RID: 26665 RVA: 0x00256D5F File Offset: 0x00254F5F
		public Calamity()
			: base(3, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x00256D6C File Offset: 0x00254F6C
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await PowerCmd.Apply<CalamityPower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x00256DAF File Offset: 0x00254FAF
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
