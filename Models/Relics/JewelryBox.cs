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
	// Token: 0x0200051E RID: 1310
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class JewelryBox : RelicModel
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06004C55 RID: 19541 RVA: 0x00215673 File Offset: 0x00213873
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06004C56 RID: 19542 RVA: 0x00215676 File Offset: 0x00213876
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromCard<Apotheosis>(false));
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06004C57 RID: 19543 RVA: 0x00215683 File Offset: 0x00213883
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004C58 RID: 19544 RVA: 0x00215688 File Offset: 0x00213888
		public override async Task AfterObtained()
		{
			CardModel cardModel = base.Owner.RunState.CreateCard<Apotheosis>(base.Owner);
			CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
			CardPileAddResult cardPileAddResult2 = cardPileAddResult;
			CardCmd.PreviewCardPileAdd(new <>z__ReadOnlySingleElementList<CardPileAddResult>(cardPileAddResult2), 2f, CardPreviewStyle.HorizontalLayout);
		}
	}
}
