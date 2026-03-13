using System;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E2 RID: 1250
	public sealed class DeprecatedRelic : RelicModel
	{
		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x00212CE2 File Offset: 0x00210EE2
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.None;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x00212CE5 File Offset: 0x00210EE5
		public override bool IsStackable
		{
			get
			{
				return true;
			}
		}
	}
}
