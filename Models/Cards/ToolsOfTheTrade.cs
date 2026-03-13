using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Cards
{
	// Token: 0x02000A9D RID: 2717
	public sealed class ToolsOfTheTrade : CardModel
	{
		// Token: 0x060071D2 RID: 29138 RVA: 0x00269F3C File Offset: 0x0026813C
		public ToolsOfTheTrade()
			: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
		{
		}

		// Token: 0x060071D3 RID: 29139 RVA: 0x00269F4C File Offset: 0x0026814C
		[NullableContext(1)]
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
			await PowerCmd.Apply<ToolsOfTheTradePower>(base.Owner.Creature, 1m, base.Owner.Creature, this, false);
		}

		// Token: 0x060071D4 RID: 29140 RVA: 0x00269F8F File Offset: 0x0026818F
		protected override void OnUpgrade()
		{
			base.EnergyCost.UpgradeBy(-1);
		}
	}
}
