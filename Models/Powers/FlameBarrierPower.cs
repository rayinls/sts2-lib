using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000621 RID: 1569
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FlameBarrierPower : PowerModel
	{
		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00221678 File Offset: 0x0021F878
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x0022167B File Offset: 0x0021F87B
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052A7 RID: 21159 RVA: 0x00221680 File Offset: 0x0021F880
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult _, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel __)
		{
			if (target == base.Owner)
			{
				if (dealer != null)
				{
					if (props.IsPoweredAttack())
					{
						await CreatureCmd.Damage(choiceContext, dealer, base.Amount, ValueProp.Unpowered, base.Owner, null);
					}
				}
			}
		}

		// Token: 0x060052A8 RID: 21160 RVA: 0x002216E8 File Offset: 0x0021F8E8
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (base.Owner.Side != side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
