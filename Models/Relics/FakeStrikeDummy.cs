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
	// Token: 0x020004FB RID: 1275
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FakeStrikeDummy : RelicModel
	{
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06004B7D RID: 19325 RVA: 0x00213D27 File Offset: 0x00211F27
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06004B7E RID: 19326 RVA: 0x00213D2A File Offset: 0x00211F2A
		public override int MerchantCost
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06004B7F RID: 19327 RVA: 0x00213D2E File Offset: 0x00211F2E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("ExtraDamage", 1m));
			}
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00213D44 File Offset: 0x00211F44
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

		// Token: 0x040021B0 RID: 8624
		private const string _extraDamageKey = "ExtraDamage";
	}
}
