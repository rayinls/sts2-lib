using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200062C RID: 1580
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FreeSkillPower : PowerModel
	{
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x060052DB RID: 21211 RVA: 0x00221C6B File Offset: 0x0021FE6B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060052DC RID: 21212 RVA: 0x00221C6E File Offset: 0x0021FE6E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x00221C74 File Offset: 0x0021FE74
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Skill)
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

		// Token: 0x060052DE RID: 21214 RVA: 0x00221CF4 File Offset: 0x0021FEF4
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Skill)
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
