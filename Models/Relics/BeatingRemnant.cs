using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B1 RID: 1201
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeatingRemnant : RelicModel
	{
		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x060049C3 RID: 18883 RVA: 0x00210A6F File Offset: 0x0020EC6F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060049C4 RID: 18884 RVA: 0x00210A72 File Offset: 0x0020EC72
		// (set) Token: 0x060049C5 RID: 18885 RVA: 0x00210A7A File Offset: 0x0020EC7A
		private decimal DamageReceivedThisTurn
		{
			get
			{
				return this._damageReceivedThisTurn;
			}
			set
			{
				base.AssertMutable();
				this._damageReceivedThisTurn = value;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060049C6 RID: 18886 RVA: 0x00210A89 File Offset: 0x0020EC89
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("MaxHpLoss", 20m));
			}
		}

		// Token: 0x060049C7 RID: 18887 RVA: 0x00210AA4 File Offset: 0x0020ECA4
		[NullableContext(2)]
		public override decimal ModifyHpLostAfterOsty([Nullable(1)] Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource)
		{
			if (!CombatManager.Instance.IsInProgress)
			{
				return amount;
			}
			if (target != base.Owner.Creature)
			{
				return amount;
			}
			return Math.Min(amount, base.DynamicVars["MaxHpLoss"].BaseValue - this.DamageReceivedThisTurn);
		}

		// Token: 0x060049C8 RID: 18888 RVA: 0x00210AF5 File Offset: 0x0020ECF5
		public override Task AfterModifyingHpLostAfterOsty()
		{
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x060049C9 RID: 18889 RVA: 0x00210B04 File Offset: 0x0020ED04
		public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (!CombatManager.Instance.IsInProgress)
			{
				return Task.CompletedTask;
			}
			if (target != base.Owner.Creature)
			{
				return Task.CompletedTask;
			}
			this.DamageReceivedThisTurn += result.UnblockedDamage;
			return Task.CompletedTask;
		}

		// Token: 0x060049CA RID: 18890 RVA: 0x00210B58 File Offset: 0x0020ED58
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			this.DamageReceivedThisTurn = 0m;
			return Task.CompletedTask;
		}

		// Token: 0x04002187 RID: 8583
		private const string _maxHpLossKey = "MaxHpLoss";

		// Token: 0x04002188 RID: 8584
		private decimal _damageReceivedThisTurn;
	}
}
