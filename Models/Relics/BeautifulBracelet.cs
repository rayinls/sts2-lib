using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004B2 RID: 1202
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BeautifulBracelet : RelicModel
	{
		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060049CC RID: 18892 RVA: 0x00210B7C File Offset: 0x0020ED7C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060049CD RID: 18893 RVA: 0x00210B7F File Offset: 0x0020ED7F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(3),
					new DynamicVar("Swift", 3m)
				});
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060049CE RID: 18894 RVA: 0x00210BA8 File Offset: 0x0020EDA8
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Swift>(base.DynamicVars["Swift"].IntValue);
			}
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x00210BC4 File Offset: 0x0020EDC4
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, base.DynamicVars.Cards.IntValue);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(base.Owner, ModelDb.Enchantment<Swift>(), base.DynamicVars["Swift"].IntValue, cardSelectorPrefs);
			IEnumerable<CardModel> enumerable2 = enumerable;
			foreach (CardModel cardModel in enumerable2)
			{
				CardCmd.Enchant<Swift>(cardModel, 3m);
				NCardEnchantVfx ncardEnchantVfx = NCardEnchantVfx.Create(cardModel);
				if (ncardEnchantVfx != null)
				{
					NRun instance = NRun.Instance;
					if (instance != null)
					{
						instance.GlobalUi.CardPreviewContainer.AddChildSafely(ncardEnchantVfx);
					}
				}
			}
		}

		// Token: 0x04002189 RID: 8585
		private const string _swiftKey = "Swift";
	}
}
