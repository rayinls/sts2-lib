using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000699 RID: 1689
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SleightOfFleshPower : PowerModel
	{
		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x002266BF File Offset: 0x002248BF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x002266C2 File Offset: 0x002248C2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012AA RID: 4778
		// (get) Token: 0x0600556A RID: 21866 RVA: 0x002266C5 File Offset: 0x002248C5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return Array.Empty<DynamicVar>();
			}
		}

		// Token: 0x0600556B RID: 21867 RVA: 0x002266CC File Offset: 0x002248CC
		public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!(amount == 0m))
			{
				if (power.GetTypeForAmount(amount) == PowerType.Debuff)
				{
					if (power.Owner.IsEnemy)
					{
						if (applier == base.Owner)
						{
							if (!(power is ITemporaryPower))
							{
								base.Flash();
								await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), power.Owner, base.Amount, ValueProp.Unpowered, base.Owner, null);
							}
						}
					}
				}
			}
		}
	}
}
