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
	// Token: 0x02000521 RID: 1313
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Kifuda : RelicModel
	{
		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06004C6F RID: 19567 RVA: 0x0021598C File Offset: 0x00213B8C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06004C70 RID: 19568 RVA: 0x0021598F File Offset: 0x00213B8F
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06004C71 RID: 19569 RVA: 0x00215992 File Offset: 0x00213B92
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06004C72 RID: 19570 RVA: 0x0021599F File Offset: 0x00213B9F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Adroit>(3);
			}
		}

		// Token: 0x06004C73 RID: 19571 RVA: 0x002159A8 File Offset: 0x00213BA8
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 0, base.DynamicVars.Cards.IntValue)
			{
				Cancelable = false,
				RequireManualConfirmation = true
			};
			Adroit canonicalEnchantment = ModelDb.Enchantment<Adroit>();
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, canonicalEnchantment, 3, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Enchant(canonicalEnchantment.ToMutable(), cardModel, 3m);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x040021CB RID: 8651
		private const int _adroitAmount = 3;
	}
}
