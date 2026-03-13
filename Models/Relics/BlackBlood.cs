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
	// Token: 0x020004B9 RID: 1209
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlackBlood : RelicModel
	{
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x060049FB RID: 18939 RVA: 0x0021117B File Offset: 0x0020F37B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060049FC RID: 18940 RVA: 0x0021117E File Offset: 0x0020F37E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(12m));
			}
		}

		// Token: 0x060049FD RID: 18941 RVA: 0x00211194 File Offset: 0x0020F394
		public override async Task AfterCombatVictory(CombatRoom _)
		{
			if (!base.Owner.Creature.IsDead)
			{
				base.Flash();
				await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.BaseValue, true);
			}
		}
	}
}
