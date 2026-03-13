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
	// Token: 0x0200065C RID: 1628
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MonologuePower : PowerModel
	{
		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x060053E6 RID: 21478 RVA: 0x00223993 File Offset: 0x00221B93
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060053E7 RID: 21479 RVA: 0x00223996 File Offset: 0x00221B96
		public override PowerStackType StackType
		{
			get
			{
				if (base.DynamicVars["StrengthApplied"].IntValue != 0)
				{
					return PowerStackType.Counter;
				}
				return PowerStackType.None;
			}
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060053E8 RID: 21480 RVA: 0x002239B2 File Offset: 0x00221BB2
		public override int DisplayAmount
		{
			get
			{
				return base.DynamicVars["StrengthApplied"].IntValue;
			}
		}

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x060053E9 RID: 21481 RVA: 0x002239C9 File Offset: 0x00221BC9
		public override bool IsInstanced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x060053EA RID: 21482 RVA: 0x002239CC File Offset: 0x00221BCC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(1m),
					new DynamicVar("StrengthApplied", 0m)
				});
			}
		}

		// Token: 0x060053EB RID: 21483 RVA: 0x002239F8 File Offset: 0x00221BF8
		protected override object InitInternalData()
		{
			return new MonologuePower.Data();
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x060053EC RID: 21484 RVA: 0x002239FF File Offset: 0x00221BFF
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x060053ED RID: 21485 RVA: 0x00223A0C File Offset: 0x00221C0C
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<MonologuePower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.DynamicVars.Strength.IntValue);
			return Task.CompletedTask;
		}

		// Token: 0x060053EE RID: 21486 RVA: 0x00223A64 File Offset: 0x00221C64
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				int num;
				if (base.GetInternalData<MonologuePower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num))
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner, num, base.Owner, null, true);
					base.DynamicVars["StrengthApplied"].BaseValue += base.DynamicVars.Strength.IntValue;
					base.InvokeDisplayAmountChanged();
				}
			}
		}

		// Token: 0x060053EF RID: 21487 RVA: 0x00223AB0 File Offset: 0x00221CB0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == base.Owner.Side)
			{
				await PowerCmd.Remove(this);
				await PowerCmd.Apply<StrengthPower>(base.Owner, -base.DynamicVars["StrengthApplied"].BaseValue, base.Owner, null, true);
			}
		}

		// Token: 0x0400225C RID: 8796
		public const string strengthAppliedKey = "StrengthApplied";

		// Token: 0x02001A34 RID: 6708
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040066C4 RID: 26308
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}
