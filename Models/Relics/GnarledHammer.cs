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
	// Token: 0x0200050D RID: 1293
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GnarledHammer : RelicModel
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x00214C18 File Offset: 0x00212E18
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x00214C1B File Offset: 0x00212E1B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new DynamicVar("SharpAmount", 3m)
				});
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x00214C44 File Offset: 0x00212E44
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Sharp>(base.DynamicVars["SharpAmount"].IntValue);
			}
		}

		// Token: 0x06004BF6 RID: 19446 RVA: 0x00214C60 File Offset: 0x00212E60
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, base.DynamicVars.Cards.IntValue)
			{
				Cancelable = false,
				RequireManualConfirmation = true
			};
			Sharp canonicalEnchantment = ModelDb.Enchantment<Sharp>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, canonicalEnchantment, base.DynamicVars["SharpAmount"].IntValue, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Enchant(canonicalEnchantment.ToMutable(), cardModel, base.DynamicVars["SharpAmount"].IntValue);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x040021BD RID: 8637
		private const string _sharpAmountKey = "SharpAmount";
	}
}
