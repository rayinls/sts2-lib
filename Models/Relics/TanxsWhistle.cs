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
	// Token: 0x020005A2 RID: 1442
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TanxsWhistle : RelicModel
	{
		// Token: 0x17001000 RID: 4096
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x0021BB8B File Offset: 0x00219D8B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x0021BB8E File Offset: 0x00219D8E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Whistle>(false));
			}
		}

		// Token: 0x06004FC8 RID: 20424 RVA: 0x0021BB9C File Offset: 0x00219D9C
		public override async Task AfterObtained()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<Whistle>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(new <>z__ReadOnlySingleElementList<CardPileAddResult>(cardPileAddResult2), 2f, CardPreviewStyle.HorizontalLayout);
		}
	}
}
