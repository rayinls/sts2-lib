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
	// Token: 0x020005BC RID: 1468
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VeryHotCocoa : RelicModel
	{
		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06005081 RID: 20609 RVA: 0x0021D16C File Offset: 0x0021B36C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x06005082 RID: 20610 RVA: 0x0021D16F File Offset: 0x0021B36F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(4));
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x06005083 RID: 20611 RVA: 0x0021D17C File Offset: 0x0021B37C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06005084 RID: 20612 RVA: 0x0021D18C File Offset: 0x0021B38C
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
				}
			}
		}
	}
}
