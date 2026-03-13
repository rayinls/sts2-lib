using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006DC RID: 1756
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockPreventDeathPower : PowerModel
	{
		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06005703 RID: 22275 RVA: 0x00229671 File Offset: 0x00227871
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06005704 RID: 22276 RVA: 0x00229674 File Offset: 0x00227874
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005705 RID: 22277 RVA: 0x00229677 File Offset: 0x00227877
		public override bool ShouldDie(Creature creature)
		{
			return creature != base.Owner;
		}

		// Token: 0x06005706 RID: 22278 RVA: 0x00229688 File Offset: 0x00227888
		public override async Task AfterPreventingDeath(Creature creature)
		{
			await CreatureCmd.Heal(creature, base.Amount, true);
			await PowerCmd.Remove(this);
		}
	}
}
