using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005EA RID: 1514
	public sealed class CalcifyPower : PowerModel
	{
		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06005171 RID: 20849 RVA: 0x0021F593 File Offset: 0x0021D793
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06005172 RID: 20850 RVA: 0x0021F596 File Offset: 0x0021D796
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005173 RID: 20851 RVA: 0x0021F59C File Offset: 0x0021D79C
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!(((dealer != null) ? dealer.Monster : null) is Osty))
			{
				return 0m;
			}
			Creature owner = base.Owner;
			Player petOwner = dealer.PetOwner;
			if (owner != ((petOwner != null) ? petOwner.Creature : null))
			{
				return 0m;
			}
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			return base.Amount;
		}
	}
}
