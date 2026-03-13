using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006BE RID: 1726
	public sealed class TerritorialPower : PowerModel
	{
		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x00228613 File Offset: 0x00226813
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06005660 RID: 22112 RVA: 0x00228616 File Offset: 0x00226816
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005661 RID: 22113 RVA: 0x0022861C File Offset: 0x0022681C
		[NullableContext(1)]
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
			}
		}
	}
}
