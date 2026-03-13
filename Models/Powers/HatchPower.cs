using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200063A RID: 1594
	public sealed class HatchPower : PowerModel
	{
		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06005331 RID: 21297 RVA: 0x002225CE File Offset: 0x002207CE
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x002225D1 File Offset: 0x002207D1
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x002225D4 File Offset: 0x002207D4
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Enemy)
			{
				await PowerCmd.TickDownDuration(this);
			}
		}
	}
}
