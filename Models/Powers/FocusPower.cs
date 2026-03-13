using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000626 RID: 1574
	public sealed class FocusPower : PowerModel
	{
		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x060052BE RID: 21182 RVA: 0x00221926 File Offset: 0x0021FB26
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x00221929 File Offset: 0x0021FB29
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x060052C0 RID: 21184 RVA: 0x0022192C File Offset: 0x0021FB2C
		public override bool AllowNegative
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060052C1 RID: 21185 RVA: 0x0022192F File Offset: 0x0021FB2F
		[NullableContext(1)]
		public override decimal ModifyOrbValue(Player player, decimal value)
		{
			if (base.Owner.Player != player)
			{
				return value;
			}
			return Math.Max(value + base.Amount, 0m);
		}
	}
}
