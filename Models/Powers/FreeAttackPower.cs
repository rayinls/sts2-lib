using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200062A RID: 1578
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FreeAttackPower : PowerModel
	{
		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060052D1 RID: 21201 RVA: 0x00221AB3 File Offset: 0x0021FCB3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x060052D2 RID: 21202 RVA: 0x00221AB6 File Offset: 0x0021FCB6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052D3 RID: 21203 RVA: 0x00221ABC File Offset: 0x0021FCBC
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Attack)
			{
				return false;
			}
			CardPile pile = card.Pile;
			PileType? pileType = ((pile != null) ? new PileType?(pile.Type) : null);
			bool flag;
			if (pileType != null)
			{
				PileType valueOrDefault = pileType.GetValueOrDefault();
				if (valueOrDefault == PileType.Hand || valueOrDefault == PileType.Play)
				{
					flag = true;
					goto IL_0066;
				}
			}
			flag = false;
			IL_0066:
			if (!flag)
			{
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x060052D4 RID: 21204 RVA: 0x00221B3C File Offset: 0x0021FD3C
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Attack)
				{
					CardPile pile = cardPlay.Card.Pile;
					PileType? pileType = ((pile != null) ? new PileType?(pile.Type) : null);
					bool flag;
					if (pileType != null)
					{
						PileType valueOrDefault = pileType.GetValueOrDefault();
						if (valueOrDefault == PileType.Hand || valueOrDefault == PileType.Play)
						{
							flag = true;
							goto IL_009A;
						}
					}
					flag = false;
					IL_009A:
					if (flag)
					{
						await PowerCmd.Decrement(this);
					}
				}
			}
		}
	}
}
