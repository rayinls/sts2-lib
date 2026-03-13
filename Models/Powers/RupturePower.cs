using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000689 RID: 1673
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RupturePower : PowerModel
	{
		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x00225AB7 File Offset: 0x00223CB7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06005503 RID: 21763 RVA: 0x00225ABA File Offset: 0x00223CBA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06005504 RID: 21764 RVA: 0x00225ABD File Offset: 0x00223CBD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00225AC9 File Offset: 0x00223CC9
		protected override object InitInternalData()
		{
			return new RupturePower.Data();
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00225AD0 File Offset: 0x00223CD0
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (base.CombatState.CurrentSide != base.Owner.Side)
			{
				return Task.CompletedTask;
			}
			base.GetInternalData<RupturePower.Data>().playedCards.Add(cardPlay.Card, 0);
			return Task.CompletedTask;
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x00225B38 File Offset: 0x00223D38
		public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, [Nullable(2)] Creature dealer, [Nullable(2)] CardModel cardSource)
		{
			if (target == base.Owner)
			{
				if (result.UnblockedDamage > 0)
				{
					if (base.CombatState.CurrentSide == base.Owner.Side)
					{
						if (cardSource == null || !base.GetInternalData<RupturePower.Data>().playedCards.ContainsKey(cardSource))
						{
							await PowerCmd.Apply<StrengthPower>(base.Owner, base.Amount, base.Owner, null, false);
						}
						else
						{
							base.GetInternalData<RupturePower.Data>().playedCards[cardSource] += base.Amount;
						}
					}
				}
			}
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00225B94 File Offset: 0x00223D94
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				int num;
				if (base.GetInternalData<RupturePower.Data>().playedCards.Remove(cardPlay.Card, out num))
				{
					await PowerCmd.Apply<StrengthPower>(base.Owner, num, base.Owner, null, false);
				}
			}
		}

		// Token: 0x02001A75 RID: 6773
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x0400680E RID: 26638
			[Nullable(1)]
			public readonly Dictionary<CardModel, int> playedCards = new Dictionary<CardModel, int>();
		}
	}
}
