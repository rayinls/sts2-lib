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
	// Token: 0x0200059B RID: 1435
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Storybook : RelicModel
	{
		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x0021B7F8 File Offset: 0x002199F8
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06004FA2 RID: 20386 RVA: 0x0021B7FB File Offset: 0x002199FB
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<BrightestFlame>(false));
			}
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x0021B808 File Offset: 0x00219A08
		public override async Task AfterObtained()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<BrightestFlame>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(new <>z__ReadOnlySingleElementList<CardPileAddResult>(cardPileAddResult2), 2f, CardPreviewStyle.HorizontalLayout);
		}
	}
}
