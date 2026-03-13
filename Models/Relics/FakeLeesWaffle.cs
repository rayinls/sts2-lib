using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F6 RID: 1270
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeLeesWaffle : RelicModel
	{
		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06004B5C RID: 19292 RVA: 0x00213A29 File Offset: 0x00211C29
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06004B5D RID: 19293 RVA: 0x00213A2C File Offset: 0x00211C2C
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06004B5E RID: 19294 RVA: 0x00213A2F File Offset: 0x00211C2F
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06004B5F RID: 19295 RVA: 0x00213A33 File Offset: 0x00211C33
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(10m));
			}
		}

		// Token: 0x06004B60 RID: 19296 RVA: 0x00213A48 File Offset: 0x00211C48
		public override async Task AfterObtained()
		{
			await CreatureCmd.Heal(base.Owner.Creature, base.Owner.Creature.MaxHp * (base.DynamicVars.Heal.BaseValue / 100m), true);
		}
	}
}
