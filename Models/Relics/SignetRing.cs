using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200058F RID: 1423
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SignetRing : RelicModel
	{
		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06004F50 RID: 20304 RVA: 0x0021AE3F File Offset: 0x0021903F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x0021AE42 File Offset: 0x00219042
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(999));
			}
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x0021AE54 File Offset: 0x00219054
		public override async Task AfterObtained()
		{
			await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
		}
	}
}
