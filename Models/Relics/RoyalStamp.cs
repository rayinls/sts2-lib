using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000581 RID: 1409
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RoyalStamp : RelicModel
	{
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x0021A2BB File Offset: 0x002184BB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06004EFD RID: 20221 RVA: 0x0021A2BE File Offset: 0x002184BE
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0021A2C1 File Offset: 0x002184C1
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(1),
					new StringVar("Enchantment", ModelDb.Enchantment<RoyallyApproved>().Title.GetFormattedText())
				});
			}
		}

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x0021A2F3 File Offset: 0x002184F3
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<RoyallyApproved>(1);
			}
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0021A2FC File Offset: 0x002184FC
		public override async Task AfterObtained()
		{
			EnchantmentModel royalStamp = ModelDb.Enchantment<RoyallyApproved>();
			List<CardModel> list = PileType.Deck.GetPile(base.Owner).Cards.Where((CardModel c) => royalStamp.CanEnchant(c)).ToList<CardModel>();
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, 1);
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForEnchantment(list.UnstableShuffle(base.Owner.RunState.Rng.Niche).ToList<CardModel>(), royalStamp, 1, cardSelectorPrefs);
			CardModel cardModel = enumerable.FirstOrDefault<CardModel>();
			if (cardModel != null)
			{
				CardCmd.Enchant<RoyallyApproved>(cardModel, 1m);
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
	}
}
