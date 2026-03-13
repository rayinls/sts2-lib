using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005D5 RID: 1493
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AfterimagePower : PowerModel
	{
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06005100 RID: 20736 RVA: 0x0021EA89 File Offset: 0x0021CC89
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x0021EA8C File Offset: 0x0021CC8C
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x0021EA8F File Offset: 0x0021CC8F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x0021EAA1 File Offset: 0x0021CCA1
		protected override object InitInternalData()
		{
			return new AfterimagePower.Data();
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x0021EAA8 File Offset: 0x0021CCA8
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<AfterimagePower.Data>().amountsForPlayedCards.Add(cardPlay.Card, base.Amount);
			return Task.CompletedTask;
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x0021EAF4 File Offset: 0x0021CCF4
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				int num;
				if (base.GetInternalData<AfterimagePower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out num))
				{
					if (num > 0)
					{
						await CreatureCmd.GainBlock(base.Owner, num, ValueProp.Unpowered, null, true);
					}
				}
			}
		}

		// Token: 0x0200199D RID: 6557
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040063E9 RID: 25577
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
		}
	}
}
