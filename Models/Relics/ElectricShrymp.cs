using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004EE RID: 1262
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ElectricShrymp : RelicModel
	{
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06004B24 RID: 19236 RVA: 0x0021347D File Offset: 0x0021167D
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x00213480 File Offset: 0x00211680
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06004B26 RID: 19238 RVA: 0x00213483 File Offset: 0x00211683
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Imbued>(1);
			}
		}

		// Token: 0x06004B27 RID: 19239 RVA: 0x0021348C File Offset: 0x0021168C
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			Imbued canonicalMomentum = ModelDb.Enchantment<Imbued>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, canonicalMomentum, 1, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Enchant(canonicalMomentum.ToMutable(), cardModel, 1m);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}
	}
}
