using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004F1 RID: 1265
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EmptyCage : RelicModel
	{
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06004B3A RID: 19258 RVA: 0x00213722 File Offset: 0x00211922
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x00213725 File Offset: 0x00211925
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06004B3C RID: 19260 RVA: 0x00213728 File Offset: 0x00211928
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x00213738 File Offset: 0x00211938
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
		}
	}
}
