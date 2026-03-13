using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200060D RID: 1549
	public sealed class DisintegrationPower : PowerModel
	{
		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06005236 RID: 21046 RVA: 0x00220A2E File Offset: 0x0021EC2E
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06005237 RID: 21047 RVA: 0x00220A31 File Offset: 0x0021EC31
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x00220A34 File Offset: 0x0021EC34
		[NullableContext(1)]
		public override async Task AfterTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await CreatureCmd.Damage(choiceContext, base.Owner, base.Amount, ValueProp.Unpowered, base.Owner, null);
				VfxCmd.PlayOnCreatureCenter(base.Owner, "vfx/vfx_attack_blunt");
			}
		}
	}
}
