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
	// Token: 0x0200056F RID: 1391
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PreciseScissors : RelicModel
	{
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06004E8D RID: 20109 RVA: 0x002196CB File Offset: 0x002178CB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06004E8E RID: 20110 RVA: 0x002196CE File Offset: 0x002178CE
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06004E8F RID: 20111 RVA: 0x002196D1 File Offset: 0x002178D1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x002196E0 File Offset: 0x002178E0
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
