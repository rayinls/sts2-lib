using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D6 RID: 1238
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChosenCheese : RelicModel
	{
		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06004AB2 RID: 19122 RVA: 0x00212753 File Offset: 0x00210953
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06004AB3 RID: 19123 RVA: 0x00212756 File Offset: 0x00210956
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(1m));
			}
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x00212768 File Offset: 0x00210968
		public override async Task AfterCombatEnd(CombatRoom _)
		{
			base.Flash();
			await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
		}
	}
}
