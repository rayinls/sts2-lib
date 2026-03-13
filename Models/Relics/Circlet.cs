using System;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D7 RID: 1239
	public sealed class Circlet : RelicModel
	{
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x06004AB6 RID: 19126 RVA: 0x002127B3 File Offset: 0x002109B3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.None;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x06004AB7 RID: 19127 RVA: 0x002127B6 File Offset: 0x002109B6
		public override bool IsStackable
		{
			get
			{
				return true;
			}
		}
	}
}
