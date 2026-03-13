using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200053D RID: 1341
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MiniatureCannon : RelicModel
	{
		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06004D2F RID: 19759 RVA: 0x00216F67 File Offset: 0x00215167
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06004D30 RID: 19760 RVA: 0x00216F6A File Offset: 0x0021516A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("ExtraDamage", 3m));
			}
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x00216F84 File Offset: 0x00215184
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			if (cardSource == null)
			{
				return 0m;
			}
			if (!cardSource.IsUpgraded)
			{
				return 0m;
			}
			if (dealer != base.Owner.Creature && cardSource.Owner != base.Owner)
			{
				return 0m;
			}
			return base.DynamicVars["ExtraDamage"].BaseValue;
		}

		// Token: 0x040021DF RID: 8671
		private const string _extraDamageKey = "ExtraDamage";
	}
}
