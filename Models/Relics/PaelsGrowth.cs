using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000555 RID: 1365
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsGrowth : RelicModel
	{
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06004DCC RID: 19916 RVA: 0x0021808F File Offset: 0x0021628F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x00218092 File Offset: 0x00216292
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StringVar("EnchantmentName", ModelDb.Enchantment<Clone>().Title.GetFormattedText()));
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x002180B2 File Offset: 0x002162B2
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Clone>(1);
			}
		}

		// Token: 0x06004DCF RID: 19919 RVA: 0x002180BC File Offset: 0x002162BC
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<Clone>(), 1, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Enchant<Clone>(cardModel, 4m);
				CardCmd.Preview(cardModel, 1.2f, CardPreviewStyle.HorizontalLayout);
			}
		}

		// Token: 0x06004DD0 RID: 19920 RVA: 0x002180FF File Offset: 0x002162FF
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			options.Add(new CloneRestSiteOption(player));
			return true;
		}
	}
}
