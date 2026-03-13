using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006A1 RID: 1697
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SoarPower : PowerModel
	{
		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x00226CAF File Offset: 0x00224EAF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x0600559B RID: 21915 RVA: 0x00226CB2 File Offset: 0x00224EB2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x00226CB5 File Offset: 0x00224EB5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("DamageDecrease", 50m));
			}
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x00226CCD File Offset: 0x00224ECD
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return base.DynamicVars["DamageDecrease"].BaseValue / 100m;
		}

		// Token: 0x04002273 RID: 8819
		private const string _damageDecreaseKey = "DamageDecrease";
	}
}
