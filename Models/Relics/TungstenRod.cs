using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B1 RID: 1457
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TungstenRod : RelicModel
	{
		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x0021C7BB File Offset: 0x0021A9BB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x0021C7BE File Offset: 0x0021A9BE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("HpLossReduction", 1m));
			}
		}

		// Token: 0x0600502B RID: 20523 RVA: 0x0021C7D4 File Offset: 0x0021A9D4
		[NullableContext(2)]
		public override decimal ModifyHpLostAfterOsty([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner.Creature)
			{
				return amount;
			}
			return Math.Max(0m, amount - base.DynamicVars["HpLossReduction"].BaseValue);
		}

		// Token: 0x0600502C RID: 20524 RVA: 0x0021C80B File Offset: 0x0021AA0B
		public override Task AfterModifyingHpLostAfterOsty()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x04002235 RID: 8757
		private const string _hpLossReductionKey = "HpLossReduction";
	}
}
