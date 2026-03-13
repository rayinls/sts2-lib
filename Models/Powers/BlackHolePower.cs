using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005E2 RID: 1506
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BlackHolePower : PowerModel
	{
		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06005141 RID: 20801 RVA: 0x0021F047 File Offset: 0x0021D247
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06005142 RID: 20802 RVA: 0x0021F04A File Offset: 0x0021D24A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x0021F050 File Offset: 0x0021D250
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Resources.StarsSpent > 0)
			{
				if (cardPlay.Card.Owner == base.Owner.Player)
				{
					if (cardPlay.IsLastInSeries)
					{
						await this.DealDamageToAllEnemies();
					}
				}
			}
		}

		// Token: 0x06005144 RID: 20804 RVA: 0x0021F09C File Offset: 0x0021D29C
		public override async Task AfterStarsGained(int amount, Player gainer)
		{
			if (amount > 0)
			{
				if (gainer == base.Owner.Player)
				{
					await this.DealDamageToAllEnemies();
				}
			}
		}

		// Token: 0x06005145 RID: 20805 RVA: 0x0021F0F0 File Offset: 0x0021D2F0
		private async Task DealDamageToAllEnemies()
		{
			base.Flash();
			await CreatureCmd.Damage(new BlockingPlayerChoiceContext(), base.CombatState.HittableEnemies, base.Amount, ValueProp.Unpowered, base.Owner, null);
		}
	}
}
