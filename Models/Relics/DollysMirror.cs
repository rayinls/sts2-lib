using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004E8 RID: 1256
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DollysMirror : RelicModel
	{
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x002130CF File Offset: 0x002112CF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06004B06 RID: 19206 RVA: 0x002130D2 File Offset: 0x002112D2
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x002130D8 File Offset: 0x002112D8
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckGeneric(base.Owner, cardSelectorPrefs, new Func<CardModel, bool>(this.Filter), null);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.PreviewCardPileAdd(await CardPileCmd.Add(base.Owner.RunState.CloneCard(cardModel), PileType.Deck, CardPilePosition.Bottom, null, false), 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x06004B08 RID: 19208 RVA: 0x0021311B File Offset: 0x0021131B
		private bool Filter(CardModel c)
		{
			return c.Type != CardType.Quest;
		}
	}
}
