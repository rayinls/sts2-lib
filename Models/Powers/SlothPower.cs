using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200069B RID: 1691
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SlothPower : PowerModel
	{
		// Token: 0x170012AE RID: 4782
		// (get) Token: 0x06005573 RID: 21875 RVA: 0x002267AF File Offset: 0x002249AF
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x002267B2 File Offset: 0x002249B2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06005575 RID: 21877 RVA: 0x002267B5 File Offset: 0x002249B5
		public override int DisplayAmount
		{
			get
			{
				return this._cardsPlayedThisTurn;
			}
		}

		// Token: 0x06005576 RID: 21878 RVA: 0x002267BD File Offset: 0x002249BD
		public override bool ShouldPlay(CardModel card, AutoPlayType _)
		{
			return card.Owner.Creature != base.Owner || this._cardsPlayedThisTurn < base.Amount;
		}

		// Token: 0x06005577 RID: 21879 RVA: 0x002267E2 File Offset: 0x002249E2
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			this._cardsPlayedThisTurn++;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06005578 RID: 21880 RVA: 0x0022681B File Offset: 0x00224A1B
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Side)
			{
				return Task.CompletedTask;
			}
			this._cardsPlayedThisTurn = 0;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x04002271 RID: 8817
		private int _cardsPlayedThisTurn;
	}
}
