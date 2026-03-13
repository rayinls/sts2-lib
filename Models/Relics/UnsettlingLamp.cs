using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B6 RID: 1462
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UnsettlingLamp : RelicModel
	{
		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x0600504B RID: 20555 RVA: 0x0021CB51 File Offset: 0x0021AD51
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x0600504C RID: 20556 RVA: 0x0021CB54 File Offset: 0x0021AD54
		// (set) Token: 0x0600504D RID: 20557 RVA: 0x0021CB5C File Offset: 0x0021AD5C
		[Nullable(2)]
		private CardModel TriggeringCard
		{
			[NullableContext(2)]
			get
			{
				return this._triggeringCard;
			}
			[NullableContext(2)]
			set
			{
				base.AssertMutable();
				this._triggeringCard = value;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x0600504E RID: 20558 RVA: 0x0021CB6B File Offset: 0x0021AD6B
		private List<PowerModel> DoubledPowers
		{
			get
			{
				base.AssertMutable();
				if (this._doubledPowers == null)
				{
					this._doubledPowers = new List<PowerModel>();
				}
				return this._doubledPowers;
			}
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x0600504F RID: 20559 RVA: 0x0021CB8C File Offset: 0x0021AD8C
		// (set) Token: 0x06005050 RID: 20560 RVA: 0x0021CB94 File Offset: 0x0021AD94
		private bool IsFinishedTriggering
		{
			get
			{
				return this._isFinishedTriggering;
			}
			set
			{
				base.AssertMutable();
				this._isFinishedTriggering = value;
			}
		}

		// Token: 0x06005051 RID: 20561 RVA: 0x0021CBA3 File Offset: 0x0021ADA3
		public override Task BeforeCombatStart()
		{
			this.TriggeringCard = null;
			this.DoubledPowers.Clear();
			this.IsFinishedTriggering = false;
			base.Status = RelicStatus.Active;
			return Task.CompletedTask;
		}

		// Token: 0x06005052 RID: 20562 RVA: 0x0021CBCC File Offset: 0x0021ADCC
		public override Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, [Nullable(2)] Creature applier, [Nullable(2)] CardModel cardSource)
		{
			if (this.TriggeringCard != null)
			{
				return Task.CompletedTask;
			}
			if (this.IsFinishedTriggering)
			{
				return Task.CompletedTask;
			}
			if (cardSource == null)
			{
				return Task.CompletedTask;
			}
			if (applier != base.Owner.Creature)
			{
				return Task.CompletedTask;
			}
			if (target.Side == base.Owner.Creature.Side)
			{
				return Task.CompletedTask;
			}
			if (!power.IsVisible)
			{
				return Task.CompletedTask;
			}
			if (power.GetTypeForAmount(amount) != PowerType.Debuff)
			{
				return Task.CompletedTask;
			}
			this.TriggeringCard = cardSource;
			this.DoubledPowers.Add(power);
			return Task.CompletedTask;
		}

		// Token: 0x06005053 RID: 20563 RVA: 0x0021CC69 File Offset: 0x0021AE69
		public override decimal ModifyPowerAmountGiven(PowerModel power, Creature giver, decimal amount, [Nullable(2)] Creature target, [Nullable(2)] CardModel cardSource)
		{
			if (this.TriggeringCard == null)
			{
				return amount;
			}
			if (cardSource != this.TriggeringCard)
			{
				return amount;
			}
			if (this.IsFinishedTriggering)
			{
				return amount;
			}
			if (this.HasDoubledTemporaryPowerSource(power))
			{
				return amount;
			}
			return amount * 2m;
		}

		// Token: 0x06005054 RID: 20564 RVA: 0x0021CCA2 File Offset: 0x0021AEA2
		public override Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card != this.TriggeringCard)
			{
				return Task.CompletedTask;
			}
			if (this.IsFinishedTriggering)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			this.IsFinishedTriggering = true;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06005055 RID: 20565 RVA: 0x0021CCDF File Offset: 0x0021AEDF
		public override Task AfterCombatEnd(CombatRoom room)
		{
			this.TriggeringCard = null;
			this.DoubledPowers.Clear();
			this.IsFinishedTriggering = false;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x06005056 RID: 20566 RVA: 0x0021CD08 File Offset: 0x0021AF08
		private bool HasDoubledTemporaryPowerSource(PowerModel power)
		{
			return this.DoubledPowers.OfType<ITemporaryPower>().Any((ITemporaryPower p) => p.InternallyAppliedPower.GetType() == power.GetType());
		}

		// Token: 0x04002239 RID: 8761
		[Nullable(2)]
		private CardModel _triggeringCard;

		// Token: 0x0400223A RID: 8762
		[Nullable(new byte[] { 2, 1 })]
		private List<PowerModel> _doubledPowers;

		// Token: 0x0400223B RID: 8763
		private bool _isFinishedTriggering;
	}
}
