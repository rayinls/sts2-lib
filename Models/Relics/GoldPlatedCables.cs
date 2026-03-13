using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000510 RID: 1296
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GoldPlatedCables : RelicModel
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06004C05 RID: 19461 RVA: 0x00214DC7 File Offset: 0x00212FC7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x00214DCA File Offset: 0x00212FCA
		public override int ModifyOrbPassiveTriggerCounts(OrbModel orb, int triggerCount)
		{
			if (orb.Owner != base.Owner)
			{
				return triggerCount;
			}
			if (orb != base.Owner.PlayerCombatState.OrbQueue.Orbs[0])
			{
				return triggerCount;
			}
			return triggerCount + 1;
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x00214DFF File Offset: 0x00212FFF
		public override Task AfterModifyingOrbPassiveTriggerCount(OrbModel orb)
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
