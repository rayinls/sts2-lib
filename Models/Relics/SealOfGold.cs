using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200058A RID: 1418
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SealOfGold : RelicModel
	{
		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x0021AA9F File Offset: 0x00218C9F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06004F2E RID: 20270 RVA: 0x0021AAA2 File Offset: 0x00218CA2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar(1),
					new GoldVar(5)
				});
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06004F2F RID: 20271 RVA: 0x0021AAC1 File Offset: 0x00218CC1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004F30 RID: 20272 RVA: 0x0021AAD0 File Offset: 0x00218CD0
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (base.Owner.Gold >= base.DynamicVars.Gold.IntValue)
				{
					base.Flash();
					await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
					await PlayerCmd.LoseGold(base.DynamicVars.Gold.IntValue, base.Owner, GoldLossType.Lost);
				}
			}
		}
	}
}
