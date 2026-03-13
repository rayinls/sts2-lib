using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000534 RID: 1332
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Mango : RelicModel
	{
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004CF8 RID: 19704 RVA: 0x00216933 File Offset: 0x00214B33
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x00216936 File Offset: 0x00214B36
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06004CFA RID: 19706 RVA: 0x00216939 File Offset: 0x00214B39
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(14m));
			}
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0021694C File Offset: 0x00214B4C
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
