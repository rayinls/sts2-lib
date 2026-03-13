using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x0200064B RID: 1611
	public sealed class IterationPower : PowerModel
	{
		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06005393 RID: 21395 RVA: 0x002231BF File Offset: 0x002213BF
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06005394 RID: 21396 RVA: 0x002231C2 File Offset: 0x002213C2
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Counter;
			}
		}

		// Token: 0x06005395 RID: 21397 RVA: 0x002231C8 File Offset: 0x002213C8
		[NullableContext(1)]
		public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
		{
			if (card.Owner.Creature == base.Owner)
			{
				if (card.Type == CardType.Status)
				{
					int num = CombatManager.Instance.History.Entries.OfType<CardDrawnEntry>().Count((CardDrawnEntry e) => e.HappenedThisTurn(base.CombatState) && e.Actor == base.Owner && e.Card.Type == CardType.Status);
					if (num <= 1)
					{
						base.Flash();
						await CardPileCmd.Draw(choiceContext, base.Amount, base.Owner.Player, false);
					}
				}
			}
		}
	}
}
