using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000AC5 RID: 2757
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockAttackCard : MockCardModel
	{
		// Token: 0x17001F88 RID: 8072
		// (get) Token: 0x06007297 RID: 29335 RVA: 0x0026B515 File Offset: 0x00269715
		public override CardType Type
		{
			get
			{
				return CardType.Attack;
			}
		}

		// Token: 0x17001F89 RID: 8073
		// (get) Token: 0x06007298 RID: 29336 RVA: 0x0026B518 File Offset: 0x00269718
		public override TargetType TargetType
		{
			get
			{
				return this._targetingType;
			}
		}

		// Token: 0x17001F8A RID: 8074
		// (get) Token: 0x06007299 RID: 29337 RVA: 0x0026B520 File Offset: 0x00269720
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DamageVar(6m, ValueProp.Move),
					new OstyDamageVar(6m, ValueProp.Move),
					new BlockVar(0m, ValueProp.Move)
				});
			}
		}

		// Token: 0x0600729A RID: 29338 RVA: 0x0026B559 File Offset: 0x00269759
		[PreserveBaseOverrides]
		public new MockAttackCard MockBlock(int block)
		{
			base.AssertMutable();
			base.DynamicVars.Block.BaseValue = block;
			return this;
		}

		// Token: 0x0600729B RID: 29339 RVA: 0x0026B578 File Offset: 0x00269778
		public MockAttackCard MockDamage(decimal damage)
		{
			base.AssertMutable();
			base.DynamicVars.Damage.BaseValue = damage;
			return this;
		}

		// Token: 0x0600729C RID: 29340 RVA: 0x0026B592 File Offset: 0x00269792
		public MockAttackCard MockOstyDamage(decimal damage)
		{
			base.AssertMutable();
			base.DynamicVars.OstyDamage.BaseValue = damage;
			return this;
		}

		// Token: 0x0600729D RID: 29341 RVA: 0x0026B5AC File Offset: 0x002697AC
		public MockAttackCard MockHitCount(int hitCount)
		{
			base.AssertMutable();
			this._hitCount = hitCount;
			return this;
		}

		// Token: 0x0600729E RID: 29342 RVA: 0x0026B5BC File Offset: 0x002697BC
		public MockAttackCard MockFromOsty()
		{
			base.AssertMutable();
			this._fromOsty = true;
			base.MockTag(CardTag.OstyAttack);
			return this;
		}

		// Token: 0x0600729F RID: 29343 RVA: 0x0026B5D4 File Offset: 0x002697D4
		public MockAttackCard MockTargetingType(TargetType targetingType)
		{
			base.AssertMutable();
			this._targetingType = targetingType;
			return this;
		}

		// Token: 0x060072A0 RID: 29344 RVA: 0x0026B5E4 File Offset: 0x002697E4
		public MockAttackCard MockUnpoweredDamage()
		{
			base.AssertMutable();
			base.DynamicVars.Damage.Props = ValueProp.Unpowered;
			base.DynamicVars.OstyDamage.Props = ValueProp.Unpowered;
			return this;
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x0026B60F File Offset: 0x0026980F
		protected override int GetBaseBlock()
		{
			return base.DynamicVars.Block.IntValue;
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x0026B624 File Offset: 0x00269824
		protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
		{
			if (this._mockSelfHpLoss > 0)
			{
				await CreatureCmd.Damage(choiceContext, base.Owner.Creature, this._mockSelfHpLoss, ValueProp.Unblockable | ValueProp.Unpowered | ValueProp.Move, this);
			}
			if (base.DynamicVars.Block.BaseValue > 0m)
			{
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay, false);
			}
			int num;
			if (base.EnergyCost.CostsX)
			{
				num = base.ResolveEnergyXValue();
			}
			else if (this.HasStarCostX)
			{
				num = base.ResolveStarXValue();
			}
			else
			{
				num = this._hitCount;
			}
			AttackCommand attackCommand = DamageCmd.Attack(this._fromOsty ? base.DynamicVars.OstyDamage.BaseValue : base.DynamicVars.Damage.BaseValue).WithHitCount(num);
			if (this._fromOsty)
			{
				attackCommand = attackCommand.FromOsty(base.Owner.Osty, this);
			}
			else
			{
				attackCommand = attackCommand.FromCard(this);
			}
			switch (this._targetingType)
			{
			case TargetType.AnyEnemy:
				ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
				attackCommand = attackCommand.Targeting(cardPlay.Target);
				break;
			case TargetType.AllEnemies:
				attackCommand = attackCommand.TargetingAllOpponents(base.CombatState);
				break;
			case TargetType.RandomEnemy:
				attackCommand = attackCommand.TargetingRandomOpponents(base.CombatState, true);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (base.DynamicVars.Damage.Props.HasFlag(ValueProp.Unpowered))
			{
				attackCommand = attackCommand.Unpowered();
			}
			await attackCommand.Execute(choiceContext);
			if (this._mockExtraLogic != null)
			{
				await this._mockExtraLogic(this);
			}
		}

		// Token: 0x060072A3 RID: 29347 RVA: 0x0026B677 File Offset: 0x00269877
		protected override void OnUpgrade()
		{
			if (this._mockUpgradeLogic != null)
			{
				this._mockUpgradeLogic(this);
				return;
			}
			base.DynamicVars.Damage.UpgradeValueBy(3m);
		}

		// Token: 0x040025E7 RID: 9703
		private int _hitCount = 1;

		// Token: 0x040025E8 RID: 9704
		private bool _fromOsty;

		// Token: 0x040025E9 RID: 9705
		private TargetType _targetingType = TargetType.AnyEnemy;
	}
}
