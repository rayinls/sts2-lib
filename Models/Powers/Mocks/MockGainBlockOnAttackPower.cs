using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006D8 RID: 1752
	public sealed class MockGainBlockOnAttackPower : PowerModel
	{
		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x060056EE RID: 22254 RVA: 0x00229547 File Offset: 0x00227747
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x060056EF RID: 22255 RVA: 0x0022954A File Offset: 0x0022774A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x00229550 File Offset: 0x00227750
		[NullableContext(1)]
		public override async Task AfterAttack(AttackCommand command)
		{
			if (command.Attacker == base.Owner)
			{
				if (command.DamageProps.HasFlag(ValueProp.Move))
				{
					await CreatureCmd.GainBlock(base.Owner, 1m, ValueProp.Unpowered, null, false);
				}
			}
		}
	}
}
