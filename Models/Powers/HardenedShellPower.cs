using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000638 RID: 1592
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HardenedShellPower : PowerModel
	{
		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x00222485 File Offset: 0x00220685
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x06005323 RID: 21283 RVA: 0x00222488 File Offset: 0x00220688
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x0022248B File Offset: 0x0022068B
		public override bool ShouldScaleInMultiplayer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x0022248E File Offset: 0x0022068E
		public override int DisplayAmount
		{
			get
			{
				return (int)Math.Max(0m, base.Amount - base.GetInternalData<HardenedShellPower.Data>().damageReceivedThisTurn);
			}
		}

		// Token: 0x06005326 RID: 21286 RVA: 0x002224BA File Offset: 0x002206BA
		protected override object InitInternalData()
		{
			return new HardenedShellPower.Data();
		}

		// Token: 0x06005327 RID: 21287 RVA: 0x002224C1 File Offset: 0x002206C1
		[NullableContext(2)]
		public override decimal ModifyHpLostBeforeOstyLate([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return amount;
			}
			if (amount == 0m)
			{
				return amount;
			}
			return Math.Min(amount, base.Amount - base.GetInternalData<HardenedShellPower.Data>().damageReceivedThisTurn);
		}

		// Token: 0x06005328 RID: 21288 RVA: 0x002224FE File Offset: 0x002206FE
		public override Task AfterModifyingHpLostBeforeOsty()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06005329 RID: 21289 RVA: 0x0022250C File Offset: 0x0022070C
		public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (result.WasFullyBlocked)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<HardenedShellPower.Data>().damageReceivedThisTurn += result.UnblockedDamage;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x0600532A RID: 21290 RVA: 0x00222562 File Offset: 0x00220762
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<HardenedShellPower.Data>().damageReceivedThisTurn = 0m;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x02001A10 RID: 6672
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006612 RID: 26130
			public decimal damageReceivedThisTurn;
		}
	}
}
