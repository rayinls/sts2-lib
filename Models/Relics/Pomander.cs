using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200056A RID: 1386
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Pomander : RelicModel
	{
		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06004E78 RID: 20088 RVA: 0x002194DF File Offset: 0x002176DF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06004E79 RID: 20089 RVA: 0x002194E2 File Offset: 0x002176E2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(1));
			}
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x002194F0 File Offset: 0x002176F0
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.UpgradeSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForUpgrade(base.Owner, cardSelectorPrefs);
			List<CardModel> list = enumerable.ToList<CardModel>();
			foreach (CardModel cardModel in list)
			{
				CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
			}
		}
	}
}
