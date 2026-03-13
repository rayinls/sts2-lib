using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000595 RID: 1429
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sozu : RelicModel
	{
		// Token: 0x17000FD9 RID: 4057
		// (get) Token: 0x06004F7B RID: 20347 RVA: 0x0021B33B File Offset: 0x0021953B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FDA RID: 4058
		// (get) Token: 0x06004F7C RID: 20348 RVA: 0x0021B33E File Offset: 0x0021953E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06004F7D RID: 20349 RVA: 0x0021B34B File Offset: 0x0021954B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004F7E RID: 20350 RVA: 0x0021B358 File Offset: 0x00219558
		public override bool ShouldProcurePotion(PotionModel potion, Player player)
		{
			return player != base.Owner;
		}

		// Token: 0x06004F7F RID: 20351 RVA: 0x0021B366 File Offset: 0x00219566
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
