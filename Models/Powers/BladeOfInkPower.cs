using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E3 RID: 1507
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BladeOfInkPower : PowerModel
	{
		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06005147 RID: 20807 RVA: 0x0021F13B File Offset: 0x0021D33B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06005148 RID: 20808 RVA: 0x0021F13E File Offset: 0x0021D33E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06005149 RID: 20809 RVA: 0x0021F141 File Offset: 0x0021D341
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x0600514A RID: 20810 RVA: 0x0021F14D File Offset: 0x0021D34D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("StrengthApplied", 0m));
			}
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x0021F164 File Offset: 0x0021D364
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				if (cardPlay.Card.Type == CardType.Attack)
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, true);
					base.DynamicVars["StrengthApplied"].BaseValue += base.Amount;
					base.InvokeDisplayAmountChanged();
				}
			}
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x0021F1B0 File Offset: 0x0021D3B0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Apply<StrengthPower>(base.Owner, -base.DynamicVars["StrengthApplied"].BaseValue, base.Owner, null, true);
				await PowerCmd.Remove(this);
			}
		}

		// Token: 0x0400224B RID: 8779
		private const string _strengthAppliedKey = "StrengthApplied";
	}
}
