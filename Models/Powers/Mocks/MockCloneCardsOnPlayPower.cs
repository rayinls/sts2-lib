using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace MegaCrit.Sts2.Core.Models.Powers.Mocks
{
	// Token: 0x020006D6 RID: 1750
	public sealed class MockCloneCardsOnPlayPower : PowerModel
	{
		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x002294D3 File Offset: 0x002276D3
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x002294D6 File Offset: 0x002276D6
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x002294DC File Offset: 0x002276DC
		[NullableContext(1)]
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner.Player)
			{
				CardModel cardModel = cardPlay.Card.CreateClone();
				await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
			}
		}
	}
}
