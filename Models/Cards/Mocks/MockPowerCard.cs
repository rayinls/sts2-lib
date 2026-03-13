using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards.Mocks
{
	// Token: 0x02000AC8 RID: 2760
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MockPowerCard : MockCardModel
	{
		// Token: 0x17001F9A RID: 8090
		// (get) Token: 0x060072C9 RID: 29385 RVA: 0x0026B8DC File Offset: 0x00269ADC
		protected override int CanonicalEnergyCost
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001F9B RID: 8091
		// (get) Token: 0x060072CA RID: 29386 RVA: 0x0026B8DF File Offset: 0x00269ADF
		public override CardType Type
		{
			get
			{
				return CardType.Power;
			}
		}

		// Token: 0x17001F9C RID: 8092
		// (get) Token: 0x060072CB RID: 29387 RVA: 0x0026B8E2 File Offset: 0x00269AE2
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x17001F9D RID: 8093
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x0026B8E5 File Offset: 0x00269AE5
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(2m),
					new BlockVar(0m, ValueProp.Move)
				});
			}
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x0026B90E File Offset: 0x00269B0E
		public override MockCardModel MockBlock(int block)
		{
			base.AssertMutable();
			base.DynamicVars.Block.BaseValue = block;
			return this;
		}

		// Token: 0x060072CE RID: 29390 RVA: 0x0026B92D File Offset: 0x00269B2D
		protected override int GetBaseBlock()
		{
			return base.DynamicVars.Block.IntValue;
		}

		// Token: 0x060072CF RID: 29391 RVA: 0x0026B940 File Offset: 0x00269B40
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
			decimal baseValue = base.DynamicVars.Strength.BaseValue;
			await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, baseValue, base.Owner.Creature, this, false);
			if (this._mockExtraLogic != null)
			{
				await this._mockExtraLogic(this);
			}
		}

		// Token: 0x060072D0 RID: 29392 RVA: 0x0026B993 File Offset: 0x00269B93
		protected override void OnUpgrade()
		{
			if (this._mockUpgradeLogic != null)
			{
				this._mockUpgradeLogic(this);
				return;
			}
			base.DynamicVars.Strength.UpgradeValueBy(1m);
		}
	}
}
