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
	// Token: 0x020005A5 RID: 1445
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TheBoot : RelicModel
	{
		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x06004FD8 RID: 20440 RVA: 0x0021BD83 File Offset: 0x00219F83
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x06004FD9 RID: 20441 RVA: 0x0021BD86 File Offset: 0x00219F86
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("DamageMinimum", 5m),
					new DynamicVar("DamageThreshold", 4m)
				});
			}
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x0021BDBC File Offset: 0x00219FBC
		[NullableContext(2)]
		public override decimal ModifyHpLostBeforeOsty([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (dealer != base.Owner.Creature)
			{
				return amount;
			}
			if (!props.IsPoweredAttack())
			{
				return amount;
			}
			if (amount < 1m)
			{
				return amount;
			}
			if (amount >= base.DynamicVars["DamageMinimum"].BaseValue)
			{
				return amount;
			}
			return base.DynamicVars["DamageMinimum"].BaseValue;
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x0021BE27 File Offset: 0x0021A027
		public override Task AfterModifyingHpLostBeforeOsty()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x04002227 RID: 8743
		private const int _damageMinimum = 5;

		// Token: 0x04002228 RID: 8744
		private const string _damageMinimumKey = "DamageMinimum";

		// Token: 0x04002229 RID: 8745
		private const string _damageThresholdKey = "DamageThreshold";
	}
}
