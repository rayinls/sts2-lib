using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200059D RID: 1437
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StrikeDummy : RelicModel
	{
		// Token: 0x17000FF0 RID: 4080
		// (get) Token: 0x06004FAA RID: 20394 RVA: 0x0021B8B7 File Offset: 0x00219AB7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000FF1 RID: 4081
		// (get) Token: 0x06004FAB RID: 20395 RVA: 0x0021B8BA File Offset: 0x00219ABA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("ExtraDamage", 3m));
			}
		}

		// Token: 0x06004FAC RID: 20396 RVA: 0x0021B8D4 File Offset: 0x00219AD4
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
			if (!cardSource.Tags.Contains(CardTag.Strike))
			{
				return 0m;
			}
			if (dealer != base.Owner.Creature && cardSource.Owner != base.Owner)
			{
				return 0m;
			}
			return base.DynamicVars["ExtraDamage"].BaseValue;
		}

		// Token: 0x0400221F RID: 8735
		private const string _extraDamageKey = "ExtraDamage";
	}
}
