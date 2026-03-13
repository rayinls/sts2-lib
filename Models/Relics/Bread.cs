using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C6 RID: 1222
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Bread : RelicModel
	{
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06004A49 RID: 19017 RVA: 0x00211AD8 File Offset: 0x0020FCD8
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x00211ADB File Offset: 0x0020FCDB
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new EnergyVar("GainEnergy", 1),
					new EnergyVar("LoseEnergy", 2)
				});
			}
		}

		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06004A4B RID: 19019 RVA: 0x00211B04 File Offset: 0x0020FD04
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x00211B14 File Offset: 0x0020FD14
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			CombatState combatState = player.Creature.CombatState;
			if (combatState != null && combatState.RoundNumber == 1)
			{
				return amount;
			}
			return amount + base.DynamicVars["GainEnergy"].BaseValue;
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x00211B68 File Offset: 0x0020FD68
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber == 1)
				{
					await PlayerCmd.LoseEnergy(base.DynamicVars["LoseEnergy"].BaseValue, base.Owner);
				}
			}
		}

		// Token: 0x04002193 RID: 8595
		private const string _gainEnergyKey = "GainEnergy";

		// Token: 0x04002194 RID: 8596
		private const string _loseEnergyKey = "LoseEnergy";
	}
}
