using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B5 RID: 1461
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UndyingSigil : RelicModel
	{
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06005046 RID: 20550 RVA: 0x0021CAA3 File Offset: 0x0021ACA3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06005047 RID: 20551 RVA: 0x0021CAA6 File Offset: 0x0021ACA6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("DamageDecrease", 0.5m));
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x0021CAC1 File Offset: 0x0021ACC1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<DoomPower>());
			}
		}

		// Token: 0x06005049 RID: 20553 RVA: 0x0021CAD0 File Offset: 0x0021ACD0
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (dealer == null)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			if (target != base.Owner.Creature)
			{
				return 1m;
			}
			if (dealer == base.Owner.Creature)
			{
				return 1m;
			}
			if (dealer.CurrentHp > dealer.GetPowerAmount<DoomPower>())
			{
				return 1m;
			}
			return base.DynamicVars["DamageDecrease"].BaseValue;
		}

		// Token: 0x04002238 RID: 8760
		private const string _damageDecrease = "DamageDecrease";
	}
}
