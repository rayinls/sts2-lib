using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.CardRewardAlternatives;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.TestSupport;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005B8 RID: 1464
	public class VakuuCardSelector : ICardSelector
	{
		// Token: 0x0600505D RID: 20573 RVA: 0x0021CDBB File Offset: 0x0021AFBB
		[NullableContext(1)]
		public Task<IEnumerable<CardModel>> GetSelectedCards(IEnumerable<CardModel> options, int minSelect, int maxSelect)
		{
			return Task.FromResult<IEnumerable<CardModel>>(options.Take(maxSelect).ToList<CardModel>());
		}

		// Token: 0x0600505E RID: 20574 RVA: 0x0021CDCE File Offset: 0x0021AFCE
		[NullableContext(1)]
		[return: Nullable(2)]
		public CardModel GetSelectedCardReward(IReadOnlyList<CardCreationResult> options, IReadOnlyList<CardRewardAlternative> alternatives)
		{
			CardCreationResult cardCreationResult = options.FirstOrDefault<CardCreationResult>();
			if (cardCreationResult == null)
			{
				return null;
			}
			return cardCreationResult.Card;
		}
	}
}
