using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000657 RID: 1623
	public sealed class MayhemPower : PowerModel
	{
		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060053CF RID: 21455 RVA: 0x0022384F File Offset: 0x00221A4F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060053D0 RID: 21456 RVA: 0x00223852 File Offset: 0x00221A52
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053D1 RID: 21457 RVA: 0x00223858 File Offset: 0x00221A58
		[NullableContext(1)]
		public override async Task BeforeHandDrawLate(Player player, PlayerChoiceContext choiceContext, CombatState combatState)
		{
			if (player == base.Owner.Player)
			{
				await CardPileCmd.AutoPlayFromDrawPile(choiceContext, base.Owner.Player, base.Amount, CardPilePosition.Top, false);
			}
		}
	}
}
