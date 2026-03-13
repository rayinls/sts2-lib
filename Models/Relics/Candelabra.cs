using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004CE RID: 1230
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Candelabra : RelicModel
	{
		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x06004A86 RID: 19078 RVA: 0x00212292 File Offset: 0x00210492
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x06004A87 RID: 19079 RVA: 0x00212295 File Offset: 0x00210495
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(2));
			}
		}

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x002122A2 File Offset: 0x002104A2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x002122B0 File Offset: 0x002104B0
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber == 2)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
			}
		}

		// Token: 0x0400219B RID: 8603
		private const int _energyRound = 2;
	}
}
