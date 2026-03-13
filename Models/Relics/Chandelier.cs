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
	// Token: 0x020004D2 RID: 1234
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Chandelier : RelicModel
	{
		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06004A9F RID: 19103 RVA: 0x00212549 File Offset: 0x00210749
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06004AA0 RID: 19104 RVA: 0x0021254C File Offset: 0x0021074C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(3));
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06004AA1 RID: 19105 RVA: 0x00212559 File Offset: 0x00210759
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x00212568 File Offset: 0x00210768
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber == 3)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x0400219F RID: 8607
		private const int _energyRound = 3;
	}
}
