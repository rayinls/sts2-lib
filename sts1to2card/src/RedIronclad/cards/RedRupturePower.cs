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
using MegaCrit.Sts2.Core.Models;

namespace sts1to2card.src.RedIronclad.cards
{
	public sealed class RedRupturePower : PowerModel
	{
		// (get) Token: 0x06005502 RID: 21762 RVA: 0x00225AB7 File Offset: 0x00223CB7
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// (get) Token: 0x06005503 RID: 21763 RVA: 0x00225ABA File Offset: 0x00223CBA
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// (get) Token: 0x06005504 RID: 21764 RVA: 0x00225ABD File Offset: 0x00223CBD
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new IHoverTip[] { HoverTipFactory.FromPower<StrengthPower>() };
			}
		}

		protected override object InitInternalData()
		{
			return new RupturePower.Data();
		}

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

		private class Data
		{
			public readonly Dictionary<CardModel, int> playedCards = new Dictionary<CardModel, int>();
		}
	}
}
