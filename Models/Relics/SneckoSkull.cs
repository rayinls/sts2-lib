using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000594 RID: 1428
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SneckoSkull : RelicModel
	{
		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x0021B2CF File Offset: 0x002194CF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000FD7 RID: 4055
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x0021B2D2 File Offset: 0x002194D2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x17000FD8 RID: 4056
		// (get) Token: 0x06004F77 RID: 20343 RVA: 0x0021B2DE File Offset: 0x002194DE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<PoisonPower>(1m));
			}
		}

		// Token: 0x06004F78 RID: 20344 RVA: 0x0021B2EF File Offset: 0x002194EF
		public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, [Nullable(2)] Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (!(power is PoisonPower))
			{
				return amount;
			}
			if (giver != base.Owner.Creature)
			{
				return amount;
			}
			return amount + base.DynamicVars.Poison.IntValue;
		}

		// Token: 0x06004F79 RID: 20345 RVA: 0x0021B326 File Offset: 0x00219526
		public override Task AfterModifyingPowerAmountGiven(PowerModel power)
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
