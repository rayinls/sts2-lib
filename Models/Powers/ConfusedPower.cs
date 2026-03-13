using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F0 RID: 1520
	public sealed class ConfusedPower : PowerModel
	{
		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06005194 RID: 20884 RVA: 0x0021F9DF File Offset: 0x0021DBDF
		public override PowerType Type
		{
			get
			{
				return PowerType.Debuff;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06005195 RID: 20885 RVA: 0x0021F9E2 File Offset: 0x0021DBE2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06005196 RID: 20886 RVA: 0x0021F9E5 File Offset: 0x0021DBE5
		// (set) Token: 0x06005197 RID: 20887 RVA: 0x0021F9ED File Offset: 0x0021DBED
		public int TestEnergyCostOverride
		{
			private get
			{
				return this._testEnergyCostOverride;
			}
			set
			{
				TestMode.AssertOn();
				base.AssertMutable();
				this._testEnergyCostOverride = value;
			}
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0021FA04 File Offset: 0x0021DC04
		[NullableContext(1)]
		public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			if (card.EnergyCost.Canonical < 0)
			{
				return Task.CompletedTask;
			}
			int num = this.NextEnergyCost();
			card.EnergyCost.SetThisCombat(num, false);
			NCard ncard = NCard.FindOnTable(card, null);
			if (ncard != null)
			{
				ncard.PlayRandomizeCostAnim();
			}
			return Task.CompletedTask;
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0021FA71 File Offset: 0x0021DC71
		private int NextEnergyCost()
		{
			if (this.TestEnergyCostOverride >= 0)
			{
				return this.TestEnergyCostOverride;
			}
			return base.Owner.Player.RunState.Rng.CombatEnergyCosts.NextInt(4);
		}

		// Token: 0x0400224D RID: 8781
		private int _testEnergyCostOverride = -1;
	}
}
