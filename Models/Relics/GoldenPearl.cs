using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200050F RID: 1295
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GoldenPearl : RelicModel
	{
		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06004C00 RID: 19456 RVA: 0x00214D65 File Offset: 0x00212F65
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06004C01 RID: 19457 RVA: 0x00214D68 File Offset: 0x00212F68
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06004C02 RID: 19458 RVA: 0x00214D6B File Offset: 0x00212F6B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(150));
			}
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00214D7C File Offset: 0x00212F7C
		public override async Task AfterObtained()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
		}
	}
}
