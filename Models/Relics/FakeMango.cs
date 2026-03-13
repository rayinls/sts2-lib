using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F7 RID: 1271
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeMango : RelicModel
	{
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06004B62 RID: 19298 RVA: 0x00213A93 File Offset: 0x00211C93
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06004B63 RID: 19299 RVA: 0x00213A96 File Offset: 0x00211C96
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x06004B64 RID: 19300 RVA: 0x00213A99 File Offset: 0x00211C99
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06004B65 RID: 19301 RVA: 0x00213A9D File Offset: 0x00211C9D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(3m));
			}
		}

		// Token: 0x06004B66 RID: 19302 RVA: 0x00213AB0 File Offset: 0x00211CB0
		public override async Task AfterObtained()
		{
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
