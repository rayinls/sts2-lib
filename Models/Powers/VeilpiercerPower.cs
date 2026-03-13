using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020006CC RID: 1740
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class VeilpiercerPower : PowerModel
	{
		// Token: 0x17001348 RID: 4936
		// (get) Token: 0x060056A5 RID: 22181 RVA: 0x00228CA3 File Offset: 0x00226EA3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x060056A6 RID: 22182 RVA: 0x00228CA6 File Offset: 0x00226EA6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x1700134A RID: 4938
		// (get) Token: 0x060056A7 RID: 22183 RVA: 0x00228CA9 File Offset: 0x00226EA9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x060056A8 RID: 22184 RVA: 0x00228CB8 File Offset: 0x00226EB8
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner)
			{
				return false;
			}
			if (!card.Keywords.Contains(CardKeyword.Ethereal))
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
					goto IL_006B;
				}
			}
			flag = false;
			IL_006B:
			if (!flag)
			{
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x060056A9 RID: 22185 RVA: 0x00228D40 File Offset: 0x00226F40
		public override async Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner.Creature == base.Owner)
			{
				if (cardPlay.Card.Keywords.Contains(CardKeyword.Ethereal))
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
							goto IL_009F;
						}
					}
					flag = false;
					IL_009F:
					if (flag)
					{
						await PowerCmd.Decrement(this);
					}
				}
			}
		}
	}
}
