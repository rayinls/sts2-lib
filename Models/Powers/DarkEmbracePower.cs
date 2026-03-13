using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x02000603 RID: 1539
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DarkEmbracePower : PowerModel
	{
		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x060051FE RID: 20990 RVA: 0x00220567 File Offset: 0x0021E767
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x060051FF RID: 20991 RVA: 0x0022056A File Offset: 0x0021E76A
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005200 RID: 20992 RVA: 0x0022056D File Offset: 0x0021E76D
		protected override object InitInternalData()
		{
			return new DarkEmbracePower.Data();
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x06005201 RID: 20993 RVA: 0x00220574 File Offset: 0x0021E774
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x00220584 File Offset: 0x0021E784
		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
		{
			if (card.Owner.Creature == base.Owner)
			{
				if (causedByEthereal)
				{
					base.GetInternalData<DarkEmbracePower.Data>().etherealCount++;
				}
				else
				{
					await CardPileCmd.Draw(choiceContext, base.Amount, base.Owner.Player, false);
				}
			}
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x002205E0 File Offset: 0x0021E7E0
		public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				DarkEmbracePower.Data data = base.GetInternalData<DarkEmbracePower.Data>();
				await CardPileCmd.Draw(choiceContext, base.Amount * data.etherealCount, base.Owner.Player, false);
				data.etherealCount = 0;
			}
		}

		// Token: 0x020019D7 RID: 6615
		[NullableContext(0)]
		private class Data
		{
			// Token: 0x040064F5 RID: 25845
			public int etherealCount;
		}
	}
}
