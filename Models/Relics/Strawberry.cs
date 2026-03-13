using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200059C RID: 1436
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Strawberry : RelicModel
	{
		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06004FA5 RID: 20389 RVA: 0x0021B853 File Offset: 0x00219A53
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06004FA6 RID: 20390 RVA: 0x0021B856 File Offset: 0x00219A56
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FEF RID: 4079
		// (get) Token: 0x06004FA7 RID: 20391 RVA: 0x0021B859 File Offset: 0x00219A59
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(7m));
			}
		}

		// Token: 0x06004FA8 RID: 20392 RVA: 0x0021B86C File Offset: 0x00219A6C
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
