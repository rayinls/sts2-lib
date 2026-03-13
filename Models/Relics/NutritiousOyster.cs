using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000549 RID: 1353
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NutritiousOyster : RelicModel
	{
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06004D77 RID: 19831 RVA: 0x00217757 File Offset: 0x00215957
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06004D78 RID: 19832 RVA: 0x0021775A File Offset: 0x0021595A
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06004D79 RID: 19833 RVA: 0x0021775D File Offset: 0x0021595D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(11m));
			}
		}

		// Token: 0x06004D7A RID: 19834 RVA: 0x00217770 File Offset: 0x00215970
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
