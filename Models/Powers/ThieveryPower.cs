using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Gold;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006C3 RID: 1731
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ThieveryPower : PowerModel
	{
		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06005675 RID: 22133 RVA: 0x002287D7 File Offset: 0x002269D7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06005676 RID: 22134 RVA: 0x002287DA File Offset: 0x002269DA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06005677 RID: 22135 RVA: 0x002287DD File Offset: 0x002269DD
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06005678 RID: 22136 RVA: 0x002287E0 File Offset: 0x002269E0
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(0));
			}
		}

		// Token: 0x06005679 RID: 22137 RVA: 0x002287F0 File Offset: 0x002269F0
		public async Task Steal()
		{
			if (base.Target != null)
			{
				if (!base.Target.IsDead)
				{
					if (base.Target.Player.Gold > 0)
					{
						int amount = Math.Min(base.Amount, base.Target.Player.Gold);
						await PlayerCmd.LoseGold(amount, base.Target.Player, GoldLossType.Stolen);
						base.DynamicVars.Gold.BaseValue += amount;
					}
				}
			}
		}
	}
}
