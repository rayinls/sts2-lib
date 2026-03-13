using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006B5 RID: 1717
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SwordSagePower : PowerModel
	{
		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06005608 RID: 22024 RVA: 0x002278AF File Offset: 0x00225AAF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06005609 RID: 22025 RVA: 0x002278B2 File Offset: 0x00225AB2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x0600560A RID: 22026 RVA: 0x002278B5 File Offset: 0x00225AB5
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<SovereignBlade>(false);
			}
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x002278C0 File Offset: 0x00225AC0
		public override Task AfterPowerAmountChanged(PowerModel power, decimal amount, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (!(power is SwordSagePower))
			{
				return Task.CompletedTask;
			}
			if (power.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			Player player = base.Owner.Player;
			IEnumerable<CardModel> enumerable;
			if (player == null)
			{
				enumerable = null;
			}
			else
			{
				PlayerCombatState playerCombatState = player.PlayerCombatState;
				enumerable = ((playerCombatState != null) ? playerCombatState.AllCards : null);
			}
			IEnumerable<CardModel> enumerable2 = enumerable ?? Array.Empty<CardModel>();
			foreach (CardModel cardModel in enumerable2)
			{
				SovereignBlade sovereignBlade = cardModel as SovereignBlade;
				if (sovereignBlade != null)
				{
					sovereignBlade.SetRepeats(base.Amount + 1);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x00227974 File Offset: 0x00225B74
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			SovereignBlade sovereignBlade = card as SovereignBlade;
			if (sovereignBlade == null)
			{
				return Task.CompletedTask;
			}
			sovereignBlade.SetRepeats(base.Amount + 1);
			return Task.CompletedTask;
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x002279C4 File Offset: 0x00225BC4
		public override Task AfterRemoved(Creature oldOwner)
		{
			Player player = oldOwner.Player;
			IEnumerable<CardModel> enumerable;
			if (player == null)
			{
				enumerable = null;
			}
			else
			{
				PlayerCombatState playerCombatState = player.PlayerCombatState;
				enumerable = ((playerCombatState != null) ? playerCombatState.AllCards : null);
			}
			IEnumerable<CardModel> enumerable2 = enumerable ?? Array.Empty<CardModel>();
			foreach (CardModel cardModel in enumerable2)
			{
				SovereignBlade sovereignBlade = cardModel as SovereignBlade;
				if (sovereignBlade != null)
				{
					sovereignBlade.SetRepeats(1m);
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x00227A48 File Offset: 0x00225C48
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner)
			{
				return false;
			}
			if (!(card is SovereignBlade))
			{
				return false;
			}
			modifiedCost += base.Amount;
			return true;
		}
	}
}
