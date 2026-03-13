using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000556 RID: 1366
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsHorn : RelicModel
	{
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06004DD2 RID: 19922 RVA: 0x00218121 File Offset: 0x00216321
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06004DD3 RID: 19923 RVA: 0x00218124 File Offset: 0x00216324
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Relax>(false));
			}
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x00218134 File Offset: 0x00216334
		public override async Task AfterObtained()
		{
			List<CardPileAddResult> results = new List<CardPileAddResult>();
			for (int i = 0; i < 2; i++)
			{
				CardModel cardModel = base.Owner.RunState.CreateCard<Relax>(base.Owner);
				CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				results.Add(cardPileAddResult2);
			}
			CardCmd.PreviewCardPileAdd(results, 2f, CardPreviewStyle.HorizontalLayout);
		}
	}
}
