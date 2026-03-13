using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000668 RID: 1640
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class OutbreakPower : PowerModel
	{
		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x06005432 RID: 21554 RVA: 0x002241F3 File Offset: 0x002223F3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x002241F6 File Offset: 0x002223F6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06005434 RID: 21556 RVA: 0x002241F9 File Offset: 0x002223F9
		public override int DisplayAmount
		{
			get
			{
				return base.GetInternalData<OutbreakPower.Data>().timesPoisoned;
			}
		}

		// Token: 0x17001214 RID: 4628
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x00224206 File Offset: 0x00222406
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new RepeatVar(3));
			}
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x00224213 File Offset: 0x00222413
		protected override object InitInternalData()
		{
			return new OutbreakPower.Data();
		}

		// Token: 0x17001215 RID: 4629
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x0022421A File Offset: 0x0022241A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<PoisonPower>());
			}
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x00224228 File Offset: 0x00222428
		public override async Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (applier == base.Owner)
			{
				if (!(amount <= 0m))
				{
					if (power is PoisonPower)
					{
						OutbreakPower.Data data = base.GetInternalData<OutbreakPower.Data>();
						data.timesPoisoned++;
						if (data.timesPoisoned >= 3)
						{
							base.InvokeDisplayAmountChanged();
							base.Flash();
							await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
							data.timesPoisoned %= 3;
						}
						base.InvokeDisplayAmountChanged();
					}
				}
			}
		}

		// Token: 0x04002260 RID: 8800
		public const int poisonThreshold = 3;

		// Token: 0x02001A46 RID: 6726
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006717 RID: 26391
			public int timesPoisoned;
		}
	}
}
