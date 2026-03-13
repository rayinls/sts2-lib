using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000544 RID: 1348
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MysticLighter : RelicModel
	{
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06004D56 RID: 19798 RVA: 0x00217404 File Offset: 0x00215604
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06004D57 RID: 19799 RVA: 0x00217407 File Offset: 0x00215607
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(9m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004D58 RID: 19800 RVA: 0x0021741C File Offset: 0x0021561C
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			if (((cardSource != null) ? cardSource.Enchantment : null) == null)
			{
				return 0m;
			}
			if (cardSource.Owner != base.Owner)
			{
				return 0m;
			}
			return base.DynamicVars.Damage.IntValue;
		}
	}
}
