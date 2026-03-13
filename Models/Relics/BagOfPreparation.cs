using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B0 RID: 1200
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BagOfPreparation : RelicModel
	{
		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x060049BF RID: 18879 RVA: 0x00210A1F File Offset: 0x0020EC1F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x060049C0 RID: 18880 RVA: 0x00210A22 File Offset: 0x0020EC22
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x00210A2F File Offset: 0x0020EC2F
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			if (player.Creature.CombatState.RoundNumber > 1)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.BaseValue;
		}
	}
}
