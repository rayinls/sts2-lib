using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000570 RID: 1392
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PreservedFog : RelicModel
	{
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x0021972B File Offset: 0x0021792B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06004E93 RID: 20115 RVA: 0x0021972E File Offset: 0x0021792E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<Folly>(false);
			}
		}

		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06004E94 RID: 20116 RVA: 0x00219736 File Offset: 0x00217936
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(5));
			}
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x00219744 File Offset: 0x00217944
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.RemoveSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForRemoval(base.Owner, cardSelectorPrefs, null);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				await CardPileCmd.RemoveFromDeck(cardModel, true);
			}
			IEnumerator<CardModel> enumerator = null;
			await CardPileCmd.AddCurseToDeck<Folly>(base.Owner);
		}
	}
}
