using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200056B RID: 1387
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotionBelt : RelicModel
	{
		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x0021953B File Offset: 0x0021773B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000F5E RID: 3934
		// (get) Token: 0x06004E7D RID: 20093 RVA: 0x0021953E File Offset: 0x0021773E
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x00219541 File Offset: 0x00217741
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("PotionSlots", 2m));
			}
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00219558 File Offset: 0x00217758
		public override async Task AfterObtained()
		{
			await PlayerCmd.GainMaxPotionCount(base.DynamicVars["PotionSlots"].IntValue, base.Owner);
		}

		// Token: 0x04002207 RID: 8711
		private const string _potionSlotsKey = "PotionSlots";
	}
}
