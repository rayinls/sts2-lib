using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedFlameBarrierPower : PowerModel
	{
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x00221678 File Offset: 0x0021F878
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x0022167B File Offset: 0x0021F87B
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

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

		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (base.Owner.Side != side)
			{
				await PowerCmd.Remove(this);
			}
		}
	}
}
