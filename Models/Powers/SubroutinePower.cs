using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006AF RID: 1711
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SubroutinePower : PowerModel
	{
		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x060055DE RID: 21982 RVA: 0x002272E7 File Offset: 0x002254E7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x060055DF RID: 21983 RVA: 0x002272EA File Offset: 0x002254EA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060055E0 RID: 21984 RVA: 0x002272ED File Offset: 0x002254ED
		protected override object InitInternalData()
		{
			return new SubroutinePower.Data();
		}

		// Token: 0x060055E1 RID: 21985 RVA: 0x002272F4 File Offset: 0x002254F4
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner.Player)
			{
				return Task.CompletedTask;
			}
			if (cardPlay.Card.Type != CardType.Power)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<SubroutinePower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x060055E2 RID: 21986 RVA: 0x00227354 File Offset: 0x00225554
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				int energy;
				if (base.GetInternalData<SubroutinePower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out energy))
				{
					if (energy > 0)
					{
						base.Flash();
						for (int i = 0; i < energy; i++)
						{
							await PlayerCmd.GainEnergy(1m, base.Owner.Player);
						}
					}
				}
			}
		}

		// Token: 0x02001AA7 RID: 6823
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x04006912 RID: 26898
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}
