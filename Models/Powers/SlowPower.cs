using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200069C RID: 1692
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlowPower : PowerModel
	{
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x0600557A RID: 21882 RVA: 0x0022684B File Offset: 0x00224A4B
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x0600557B RID: 21883 RVA: 0x0022684E File Offset: 0x00224A4E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x0600557C RID: 21884 RVA: 0x00226851 File Offset: 0x00224A51
		public override int DisplayAmount
		{
			get
			{
				return base.DynamicVars["SlowAmount"].IntValue * 10;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x0600557D RID: 21885 RVA: 0x0022686B File Offset: 0x00224A6B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("SlowAmount", 0m));
			}
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x00226884 File Offset: 0x00224A84
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			DynamicVar dynamicVar = base.DynamicVars["SlowAmount"];
			decimal baseValue = dynamicVar.BaseValue;
			dynamicVar.BaseValue = baseValue + 1m;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x002268C0 File Offset: 0x00224AC0
		[NullableContext(2)]
		public override decimal ModifyDamageMultiplicative(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return 1m;
			}
			if (!props.IsPoweredAttack())
			{
				return 1m;
			}
			return 1m + 0.1m * base.DynamicVars["SlowAmount"].BaseValue;
		}

		// Token: 0x06005580 RID: 21888 RVA: 0x00226918 File Offset: 0x00224B18
		public override Task AfterModifyingDamageAmount([Nullable(2)] CardModel cardSource)
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06005581 RID: 21889 RVA: 0x00226925 File Offset: 0x00224B25
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Side)
			{
				return Task.CompletedTask;
			}
			base.DynamicVars["SlowAmount"].BaseValue = 0m;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x04002272 RID: 8818
		private const string _slowAmountKey = "SlowAmount";
	}
}
