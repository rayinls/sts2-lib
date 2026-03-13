using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000675 RID: 1653
	public sealed class PoisonPower : PowerModel
	{
		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06005483 RID: 21635 RVA: 0x00224ACB File Offset: 0x00222CCB
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06005484 RID: 21636 RVA: 0x00224ACE File Offset: 0x00222CCE
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06005485 RID: 21637 RVA: 0x00224AD1 File Offset: 0x00222CD1
		public override Color AmountLabelColor
		{
			get
			{
				return PowerModel._normalAmountLabelColor;
			}
		}

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06005486 RID: 21638 RVA: 0x00224AD8 File Offset: 0x00222CD8
		private int TriggerCount
		{
			get
			{
				IEnumerable<Creature> enumerable = from c in base.Owner.CombatState.GetOpponentsOf(base.Owner)
					where c.IsAlive
					select c;
				return Math.Min(base.Amount, 1 + enumerable.Sum((Creature a) => a.GetPowerAmount<AccelerantPower>()));
			}
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x00224B54 File Offset: 0x00222D54
		public int CalculateTotalDamageNextTurn()
		{
			decimal num = 0m;
			int num2 = Math.Min(base.Amount, this.TriggerCount);
			for (int i = 0; i < num2; i++)
			{
				decimal num3 = base.Amount - i;
				IEnumerable<AbstractModel> enumerable;
				num3 = Hook.ModifyDamage(base.Owner.CombatState.RunState, base.Owner.CombatState, base.Owner, null, num3, ValueProp.Unblockable | ValueProp.Unpowered, null, ModifyDamageHookType.All, CardPreviewMode.None, out enumerable);
				num += num3;
			}
			return (int)num;
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x00224BD4 File Offset: 0x00222DD4
		[NullableContext(1)]
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				int iterations = this.TriggerCount;
				for (int i = 0; i < iterations; i++)
				{
					await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
					if (base.Owner.IsAlive)
					{
						await PowerCmd.Decrement(this);
					}
					else
					{
						await Cmd.CustomScaledWait(0.1f, 0.25f, false, default(CancellationToken));
					}
				}
			}
		}
	}
}
