using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200052B RID: 1323
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LeesWaffle : RelicModel
	{
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x06004CC1 RID: 19649 RVA: 0x00216363 File Offset: 0x00214563
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06004CC2 RID: 19650 RVA: 0x00216366 File Offset: 0x00214566
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06004CC3 RID: 19651 RVA: 0x00216369 File Offset: 0x00214569
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(7m));
			}
		}

		// Token: 0x06004CC4 RID: 19652 RVA: 0x0021637C File Offset: 0x0021457C
		public override async Task AfterObtained()
		{
			Creature creature = base.Owner.Creature;
			await CreatureCmd.GainMaxHp(creature, base.DynamicVars.MaxHp.BaseValue);
			await CreatureCmd.Heal(creature, creature.MaxHp - creature.CurrentHp, true);
		}
	}
}
