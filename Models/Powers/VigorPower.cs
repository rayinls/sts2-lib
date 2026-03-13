using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006CE RID: 1742
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VigorPower : PowerModel
	{
		// Token: 0x1700134E RID: 4942
		// (get) Token: 0x060056B0 RID: 22192 RVA: 0x00228E0B File Offset: 0x0022700B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700134F RID: 4943
		// (get) Token: 0x060056B1 RID: 22193 RVA: 0x00228E0E File Offset: 0x0022700E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060056B2 RID: 22194 RVA: 0x00228E11 File Offset: 0x00227011
		protected override object InitInternalData()
		{
			return new VigorPower.Data();
		}

		// Token: 0x060056B3 RID: 22195 RVA: 0x00228E18 File Offset: 0x00227018
		public override Task BeforeAttack(AttackCommand command)
		{
			if (command.Attacker != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!command.DamageProps.IsPoweredAttack())
			{
				return Task.CompletedTask;
			}
			VigorPower.Data internalData = base.GetInternalData<VigorPower.Data>();
			if (internalData.commandToModify != null)
			{
				return Task.CompletedTask;
			}
			if (command.ModelSource != null && !(command.ModelSource is CardModel))
			{
				return Task.CompletedTask;
			}
			if (!command.DamageProps.IsPoweredAttack())
			{
				return Task.CompletedTask;
			}
			internalData.commandToModify = command;
			internalData.amountWhenAttackStarted = base.Amount;
			return Task.CompletedTask;
		}

		// Token: 0x060056B4 RID: 22196 RVA: 0x00228EA8 File Offset: 0x002270A8
		[NullableContext(2)]
		public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (base.Owner != dealer)
			{
				return 0m;
			}
			if (!props.IsPoweredAttack())
			{
				return 0m;
			}
			VigorPower.Data internalData = base.GetInternalData<VigorPower.Data>();
			if (internalData.commandToModify != null && cardSource != null && cardSource != internalData.commandToModify.ModelSource)
			{
				return 0m;
			}
			if (internalData.commandToModify != null && internalData.commandToModify.Attacker != dealer)
			{
				return 0m;
			}
			return base.Amount;
		}

		// Token: 0x060056B5 RID: 22197 RVA: 0x00228F24 File Offset: 0x00227124
		public override async Task AfterAttack(AttackCommand command)
		{
			VigorPower.Data internalData = base.GetInternalData<VigorPower.Data>();
			if (command == internalData.commandToModify)
			{
				await PowerCmd.ModifyAmount(this, -internalData.amountWhenAttackStarted, null, null, false);
			}
		}

		// Token: 0x02001AD6 RID: 6870
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006A20 RID: 27168
			[Nullable(2)]
			public AttackCommand commandToModify;

			// Token: 0x04006A21 RID: 27169
			public int amountWhenAttackStarted;
		}
	}
}
