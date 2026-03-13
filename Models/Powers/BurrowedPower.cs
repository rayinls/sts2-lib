using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E7 RID: 1511
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BurrowedPower : PowerModel
	{
		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x0600515F RID: 20831 RVA: 0x0021F363 File Offset: 0x0021D563
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06005160 RID: 20832 RVA: 0x0021F366 File Offset: 0x0021D566
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x06005161 RID: 20833 RVA: 0x0021F369 File Offset: 0x0021D569
		public override bool ShouldClearBlock(Creature creature)
		{
			return base.Owner != creature;
		}

		// Token: 0x06005162 RID: 20834 RVA: 0x0021F378 File Offset: 0x0021D578
		public override async Task AfterBlockBroken(Creature creature)
		{
			if (creature == base.Owner)
			{
				await CreatureCmd.TriggerAnim(base.Owner, "UnburrowAttack", 0.25f);
				await CreatureCmd.Stun(base.Owner, "BITE_MOVE");
				await PowerCmd.Remove<BurrowedPower>(base.Owner);
			}
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x0021F3C4 File Offset: 0x0021D5C4
		public override async Task AfterRemoved(Creature oldOwner)
		{
			await CreatureCmd.LoseBlock(oldOwner, 999m);
		}
	}
}
