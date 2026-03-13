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
	// Token: 0x02000524 RID: 1316
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Lantern : RelicModel
	{
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06004C93 RID: 19603 RVA: 0x00215D9C File Offset: 0x00213F9C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06004C94 RID: 19604 RVA: 0x00215D9F File Offset: 0x00213F9F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06004C95 RID: 19605 RVA: 0x00215DAC File Offset: 0x00213FAC
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004C96 RID: 19606 RVA: 0x00215DBC File Offset: 0x00213FBC
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
