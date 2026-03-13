using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000669 RID: 1641
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PagestormPower : PowerModel
	{
		// Token: 0x17001216 RID: 4630
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x0022428B File Offset: 0x0022248B
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001217 RID: 4631
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x0022428E File Offset: 0x0022248E
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x17001218 RID: 4632
		// (get) Token: 0x0600543C RID: 21564 RVA: 0x00224291 File Offset: 0x00222491
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x002242A0 File Offset: 0x002224A0
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner.Creature == base.Owner)
			{
				if (card.Keywords.Contains(CardKeyword.Ethereal))
				{
					base.Flash();
					await CardPileCmd.Draw(choiceContext, base.Amount, base.Owner.Player, false);
				}
			}
		}
	}
}
