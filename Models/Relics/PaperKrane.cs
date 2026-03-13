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
	// Token: 0x0200055D RID: 1373
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaperKrane : RelicModel
	{
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06004E1A RID: 19994 RVA: 0x002189CB File Offset: 0x00216BCB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06004E1B RID: 19995 RVA: 0x002189CE File Offset: 0x00216BCE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x06004E1C RID: 19996 RVA: 0x002189DA File Offset: 0x00216BDA
		[NullableContext(2)]
		public decimal ModifyWeakMultiplier([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner.Creature)
			{
				return amount;
			}
			if (!props.IsPoweredAttack())
			{
				return amount;
			}
			return amount - 0.15m;
		}
	}
}
