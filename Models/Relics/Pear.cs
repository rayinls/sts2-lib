using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000560 RID: 1376
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pear : RelicModel
	{
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06004E26 RID: 20006 RVA: 0x00218ADF File Offset: 0x00216CDF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06004E27 RID: 20007 RVA: 0x00218AE2 File Offset: 0x00216CE2
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x00218AE5 File Offset: 0x00216CE5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(10m));
			}
		}

		// Token: 0x06004E29 RID: 20009 RVA: 0x00218AF8 File Offset: 0x00216CF8
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
