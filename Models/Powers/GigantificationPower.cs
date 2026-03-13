using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000631 RID: 1585
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GigantificationPower : PowerModel
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060052F6 RID: 21238 RVA: 0x00221F63 File Offset: 0x00220163
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060052F7 RID: 21239 RVA: 0x00221F66 File Offset: 0x00220166
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052F8 RID: 21240 RVA: 0x00221F69 File Offset: 0x00220169
		protected override object InitInternalData()
		{
			return new GigantificationPower.Data();
		}

		// Token: 0x060052F9 RID: 21241 RVA: 0x00221F70 File Offset: 0x00220170
		public override Task BeforeAttack(AttackCommand command)
		{
			CardModel cardModel = command.ModelSource as CardModel;
			if (cardModel == null)
			{
				return Task.CompletedTask;
			}
			if (cardModel.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (cardModel.Type != CardType.Attack)
			{
				return Task.CompletedTask;
			}
			if (!command.DamageProps.IsPoweredAttack())
			{
				return Task.CompletedTask;
			}
			GigantificationPower.Data internalData = base.GetInternalData<GigantificationPower.Data>();
			if (internalData.commandToModify != null)
			{
				return Task.CompletedTask;
			}
			internalData.commandToModify = command;
			return Task.CompletedTask;
		}

		// Token: 0x060052FA RID: 21242 RVA: 0x00221FF0 File Offset: 0x002201F0
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (cardSource == null)
			{
				return 1m;
			}
			if (cardSource.Owner.Creature != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			GigantificationPower.Data internalData = base.GetInternalData<GigantificationPower.Data>();
			if (internalData.commandToModify != null && cardSource != internalData.commandToModify.ModelSource)
			{
				return 1m;
			}
			return 3m;
		}

		// Token: 0x060052FB RID: 21243 RVA: 0x0022205C File Offset: 0x0022025C
		public override async Task AfterAttack(AttackCommand command)
		{
			GigantificationPower.Data internalData = base.GetInternalData<GigantificationPower.Data>();
			if (command == internalData.commandToModify)
			{
				internalData.commandToModify = null;
				await PowerCmd.Decrement(this);
			}
		}

		// Token: 0x02001A04 RID: 6660
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040065DF RID: 26079
			[Nullable(2)]
			public AttackCommand commandToModify;
		}
	}
}
