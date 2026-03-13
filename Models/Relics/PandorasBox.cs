using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Screens;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200055B RID: 1371
	public sealed class PandorasBox : RelicModel
	{
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06004E11 RID: 19985 RVA: 0x002188E7 File Offset: 0x00216AE7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06004E12 RID: 19986 RVA: 0x002188EA File Offset: 0x00216AEA
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x002188F0 File Offset: 0x00216AF0
		[NullableContext(1)]
		public override async Task AfterObtained()
		{
			List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.IsBasicStrikeOrDefend && c.IsRemovable).ToList<CardModel>();
			IEnumerable<CardTransformation> enumerable = list.Select((CardModel c) => new CardTransformation(c, CardFactory.CreateRandomCardForTransform(c, false, base.Owner.RunState.Rng.Niche)));
			IEnumerable<CardPileAddResult> enumerable2 = await CardCmd.Transform(enumerable, null, CardPreviewStyle.None);
			List<CardPileAddResult> list2 = enumerable2.ToList<CardPileAddResult>();
			if (list2.Count > 0 && LocalContext.IsMe(base.Owner))
			{
				NSimpleCardsViewScreen.ShowScreen(list2, new LocString("relics", "PANDORAS_BOX.infoText"));
			}
		}
	}
}
