using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000620 RID: 1568
	public sealed class FeralPower : PowerModel
	{
		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x0600529A RID: 21146 RVA: 0x002214DF File Offset: 0x0021F6DF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x0600529B RID: 21147 RVA: 0x002214E2 File Offset: 0x0021F6E2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x0600529C RID: 21148 RVA: 0x002214E5 File Offset: 0x0021F6E5
		public override int DisplayAmount
		{
			get
			{
				return Math.Max(0, base.Amount - base.GetInternalData<FeralPower.Data>().zeroCostAttacksPlayed);
			}
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x002214FF File Offset: 0x0021F6FF
		[NullableContext(1)]
		protected override object InitInternalData()
		{
			return new FeralPower.Data();
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00221506 File Offset: 0x0021F706
		[NullableContext(2)]
		[return: Nullable(1)]
		public override Task AfterApplied(Creature applier, CardModel cardSource)
		{
			this.SetZeroCostAttacksPlayed(CombatManager.Instance.History.Entries.OfType<CardPlayStartedEntry>().Count((CardPlayStartedEntry e) => e.CardPlay.Card.Type == CardType.Attack && e.CardPlay.Card.Owner.Creature == base.Owner && e.CardPlay.Resources.EnergyValue == 0 && e.HappenedThisTurn(base.CombatState)));
			return Task.CompletedTask;
		}

		// Token: 0x0600529F RID: 21151 RVA: 0x00221538 File Offset: 0x0021F738
		public override ValueTuple<PileType, CardPilePosition> ModifyCardPlayResultPileTypeAndPosition([Nullable(1)] CardModel card, bool isAutoPlay, ResourceInfo resources, PileType pileType, CardPilePosition position)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			if (card.Type != CardType.Attack)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			if (resources.EnergyValue > 0)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			if (base.GetInternalData<FeralPower.Data>().zeroCostAttacksPlayed >= base.Amount)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			return new ValueTuple<PileType, CardPilePosition>(PileType.Hand, CardPilePosition.Top);
		}

		// Token: 0x060052A0 RID: 21152 RVA: 0x002215AD File Offset: 0x0021F7AD
		[NullableContext(1)]
		public override Task AfterModifyingCardPlayResultPileOrPosition(CardModel card, PileType pileType, CardPilePosition position)
		{
			base.Flash();
			this.SetZeroCostAttacksPlayed(base.GetInternalData<FeralPower.Data>().zeroCostAttacksPlayed + 1);
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x060052A1 RID: 21153 RVA: 0x002215D3 File Offset: 0x0021F7D3
		[NullableContext(1)]
		public override Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Side)
			{
				return Task.CompletedTask;
			}
			this.SetZeroCostAttacksPlayed(0);
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x060052A2 RID: 21154 RVA: 0x002215FB File Offset: 0x0021F7FB
		private void SetZeroCostAttacksPlayed(int value)
		{
			base.GetInternalData<FeralPower.Data>().zeroCostAttacksPlayed = value;
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x020019F3 RID: 6643
		private class Data
		{
			// Token: 0x04006589 RID: 25993
			public int zeroCostAttacksPlayed;
		}
	}
}
