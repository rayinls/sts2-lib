using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004ED RID: 1261
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Ectoplasm : RelicModel
	{
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06004B1E RID: 19230 RVA: 0x00213422 File Offset: 0x00211622
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x00213425 File Offset: 0x00211625
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x06004B20 RID: 19232 RVA: 0x00213432 File Offset: 0x00211632
		public override bool ShouldGainGold(decimal amount, Player player)
		{
			return player != base.Owner;
		}

		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x00213440 File Offset: 0x00211640
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004B22 RID: 19234 RVA: 0x0021344D File Offset: 0x0021164D
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.IntValue;
		}
	}
}
