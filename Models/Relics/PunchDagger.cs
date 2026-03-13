using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000573 RID: 1395
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PunchDagger : RelicModel
	{
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x00219945 File Offset: 0x00217B45
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x00219948 File Offset: 0x00217B48
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Momentum", 5m));
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x0021995F File Offset: 0x00217B5F
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x00219962 File Offset: 0x00217B62
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Momentum>(base.DynamicVars["Momentum"].IntValue);
			}
		}

		// Token: 0x06004EAB RID: 20139 RVA: 0x00219980 File Offset: 0x00217B80
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			Momentum canonicalMomentum = ModelDb.Enchantment<Momentum>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, canonicalMomentum, base.DynamicVars["Momentum"].IntValue, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Enchant(canonicalMomentum.ToMutable(), cardModel, base.DynamicVars["Momentum"].IntValue);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x04002209 RID: 8713
		private const string _momentumKey = "Momentum";
	}
}
