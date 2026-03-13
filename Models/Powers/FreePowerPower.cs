using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200062B RID: 1579
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FreePowerPower : PowerModel
	{
		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x060052D6 RID: 21206 RVA: 0x00221B8F File Offset: 0x0021FD8F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x060052D7 RID: 21207 RVA: 0x00221B92 File Offset: 0x0021FD92
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052D8 RID: 21208 RVA: 0x00221B98 File Offset: 0x0021FD98
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Power)
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

		// Token: 0x060052D9 RID: 21209 RVA: 0x00221C18 File Offset: 0x0021FE18
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Power)
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
