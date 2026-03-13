using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005BA RID: 1466
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VelvetChoker : RelicModel
	{
		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x0600506C RID: 20588 RVA: 0x0021CF5C File Offset: 0x0021B15C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x0600506D RID: 20589 RVA: 0x0021CF5F File Offset: 0x0021B15F
		public override bool ShowCounter
		{
			get
			{
				return CombatManager.Instance.IsInProgress;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x0021CF6B File Offset: 0x0021B16B
		public override int DisplayAmount
		{
			get
			{
				if (!base.IsCanonical)
				{
					return this._cardsPlayedThisTurn;
				}
				return 0;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x0021CF7D File Offset: 0x0021B17D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(6),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06005070 RID: 20592 RVA: 0x0021CF9C File Offset: 0x0021B19C
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06005071 RID: 20593 RVA: 0x0021CFA9 File Offset: 0x0021B1A9
		private bool ShouldPreventCardPlay
		{
			get
			{
				return this._cardsPlayedThisTurn >= base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0021CFC6 File Offset: 0x0021B1C6
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.BaseValue;
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0021CFE9 File Offset: 0x0021B1E9
		public override bool ShouldPlay(CardModel card, AutoPlayType _)
		{
			return card.Owner != base.Owner || !this.ShouldPreventCardPlay;
		}

		// Token: 0x06005074 RID: 20596 RVA: 0x0021D004 File Offset: 0x0021B204
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				this._cardsPlayedThisTurn++;
			}
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06005075 RID: 20597 RVA: 0x0021D032 File Offset: 0x0021B232
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			this._cardsPlayedThisTurn = 0;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06005076 RID: 20598 RVA: 0x0021D054 File Offset: 0x0021B254
		public override Task AfterCombatEnd(CombatRoom room)
		{
			this._cardsPlayedThisTurn = 0;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06005077 RID: 20599 RVA: 0x0021D068 File Offset: 0x0021B268
		public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side != base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			this._cardsPlayedThisTurn = 0;
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x0400223E RID: 8766
		private int _cardsPlayedThisTurn;
	}
}
