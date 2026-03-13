using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200055E RID: 1374
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaperPhrog : RelicModel
	{
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06004E1E RID: 19998 RVA: 0x00218A0F File Offset: 0x00216C0F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06004E1F RID: 19999 RVA: 0x00218A12 File Offset: 0x00216C12
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00218A1E File Offset: 0x00216C1E
		[NullableContext(2)]
		public decimal ModifyVulnerableMultiplier([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target == base.Owner.Creature)
			{
				return amount;
			}
			if (!props.IsPoweredAttack())
			{
				return amount;
			}
			return amount + 0.25m;
		}
	}
}
