using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000551 RID: 1361
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsBlood : RelicModel
	{
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06004DAC RID: 19884 RVA: 0x00217CE7 File Offset: 0x00215EE7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06004DAD RID: 19885 RVA: 0x00217CEA File Offset: 0x00215EEA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x00217CF7 File Offset: 0x00215EF7
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.IntValue;
		}
	}
}
