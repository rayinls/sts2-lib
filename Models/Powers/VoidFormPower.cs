using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006D0 RID: 1744
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VoidFormPower : PowerModel
	{
		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x060056BF RID: 22207 RVA: 0x0022902E File Offset: 0x0022722E
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x060056C0 RID: 22208 RVA: 0x00229031 File Offset: 0x00227231
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060056C1 RID: 22209 RVA: 0x00229034 File Offset: 0x00227234
		protected override object InitInternalData()
		{
			return new VoidFormPower.Data();
		}

		// Token: 0x060056C2 RID: 22210 RVA: 0x0022903B File Offset: 0x0022723B
		public override Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (power != this)
			{
				return Task.CompletedTask;
			}
			this.HideTemporaryZeroCostVisual();
			return Task.CompletedTask;
		}

		// Token: 0x060056C3 RID: 22211 RVA: 0x00229052 File Offset: 0x00227252
		public override Task BeforeApplied(Creature target, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			this.HideTemporaryZeroCostVisual();
			return Task.CompletedTask;
		}

		// Token: 0x060056C4 RID: 22212 RVA: 0x0022905F File Offset: 0x0022725F
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (this.ShouldSkip(card))
			{
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x060056C5 RID: 22213 RVA: 0x0022907B File Offset: 0x0022727B
		public override bool TryModifyStarCost(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (this.ShouldSkip(card))
			{
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x060056C6 RID: 22214 RVA: 0x00229098 File Offset: 0x00227298
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner && cardPlay != null && !cardPlay.IsAutoPlay && cardPlay.IsLastInSeries)
			{
				base.GetInternalData<VoidFormPower.Data>().cardsPlayedThisTurn++;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x002290E8 File Offset: 0x002272E8
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Side)
			{
				base.GetInternalData<VoidFormPower.Data>().cardsPlayedThisTurn = 0;
			}
			return Task.CompletedTask;
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x0022910C File Offset: 0x0022730C
		private bool ShouldSkip(CardModel card)
		{
			bool flag = card.Owner.Creature != base.Owner;
			bool flag2 = flag;
			if (!flag2)
			{
				CardPile pile = card.Pile;
				PileType? pileType = ((pile != null) ? new PileType?(pile.Type) : null);
				bool flag3;
				if (pileType != null)
				{
					PileType valueOrDefault = pileType.GetValueOrDefault();
					if (valueOrDefault == PileType.Hand || valueOrDefault == PileType.Play)
					{
						flag3 = true;
						goto IL_005E;
					}
				}
				flag3 = false;
				IL_005E:
				flag2 = !flag3;
			}
			return flag2 || base.GetInternalData<VoidFormPower.Data>().cardsPlayedThisTurn >= base.Amount;
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x00229198 File Offset: 0x00227398
		private void HideTemporaryZeroCostVisual()
		{
			base.GetInternalData<VoidFormPower.Data>().cardsPlayedThisTurn = 999999999;
		}

		// Token: 0x02001ADA RID: 6874
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006A30 RID: 27184
			public int cardsPlayedThisTurn;
		}
	}
}
