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
	// Token: 0x02000545 RID: 1349
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NeowsTorment : RelicModel
	{
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06004D5A RID: 19802 RVA: 0x0021747F File Offset: 0x0021567F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06004D5B RID: 19803 RVA: 0x00217482 File Offset: 0x00215682
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<NeowsFury>(false));
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06004D5C RID: 19804 RVA: 0x0021748F File Offset: 0x0021568F
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004D5D RID: 19805 RVA: 0x00217494 File Offset: 0x00215694
		public override async Task AfterObtained()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<NeowsFury>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(new <>z__ReadOnlySingleElementList<CardPileAddResult>(cardPileAddResult2), 2f, CardPreviewStyle.HorizontalLayout);
		}
	}
}
