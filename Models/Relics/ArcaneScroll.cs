using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004AB RID: 1195
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ArcaneScroll : RelicModel
	{
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x00210433 File Offset: 0x0020E633
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004995 RID: 18837 RVA: 0x00210436 File Offset: 0x0020E636
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x00210444 File Offset: 0x0020E644
		public override async Task AfterObtained()
		{
			CardCreationOptions cardCreationOptions = new CardCreationOptions(new <>z__ReadOnlySingleElementList<CardPoolModel>(base.Owner.Character.CardPool), CardCreationSource.Other, CardRarityOddsType.Uniform, (CardModel c) => c.Rarity == CardRarity.Rare).WithFlags(CardCreationFlags.NoUpgradeRoll);
			List<CardModel> list = (from r in CardFactory.CreateForReward(base.Owner, base.DynamicVars.Cards.IntValue, cardCreationOptions)
				select r.Card).ToList<CardModel>();
			if (list.Count > 0)
			{
				CardModel cardModel = list[0];
				CardPileAddResult cardPileAddResult = await CardPileCmd.Add(cardModel, PileType.Deck, CardPilePosition.Bottom, null, false);
				CardPileAddResult cardPileAddResult2 = cardPileAddResult;
				CardCmd.PreviewCardPileAdd(cardPileAddResult2, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}
	}
}
