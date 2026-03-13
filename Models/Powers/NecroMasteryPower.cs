using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200065D RID: 1629
	public sealed class NecroMasteryPower : PowerModel
	{
		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x060053F1 RID: 21489 RVA: 0x00223B03 File Offset: 0x00221D03
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x060053F2 RID: 21490 RVA: 0x00223B06 File Offset: 0x00221D06
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060053F3 RID: 21491 RVA: 0x00223B0C File Offset: 0x00221D0C
		[NullableContext(1)]
		public override async Task AfterCurrentHpChanged(Creature creature, decimal delta)
		{
			if (!(delta >= 0m))
			{
				if (creature.Monster is Osty)
				{
					if (creature.PetOwner == base.Owner.Player)
					{
						await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), creature.CombatState.HittableEnemies, -delta * base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, base.Owner, null);
					}
				}
			}
		}
	}
}
