using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E6 RID: 1510
	public sealed class BufferPower : PowerModel
	{
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600515A RID: 20826 RVA: 0x0021F2FF File Offset: 0x0021D4FF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600515B RID: 20827 RVA: 0x0021F302 File Offset: 0x0021D502
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x0021F305 File Offset: 0x0021D505
		[NullableContext(2)]
		public override decimal ModifyHpLostAfterOstyLate([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return amount;
			}
			return 0m;
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x0021F318 File Offset: 0x0021D518
		[NullableContext(1)]
		public override async Task AfterModifyingHpLostAfterOsty()
		{
			await PowerCmd.Decrement(this);
		}
	}
}
