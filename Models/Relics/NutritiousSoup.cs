using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200054A RID: 1354
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class NutritiousSoup : RelicModel
	{
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06004D7C RID: 19836 RVA: 0x002177BB File Offset: 0x002159BB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06004D7D RID: 19837 RVA: 0x002177BE File Offset: 0x002159BE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<TezcatarasEmber>(1);
			}
		}

		// Token: 0x06004D7E RID: 19838 RVA: 0x002177C8 File Offset: 0x002159C8
		public override Task AfterObtained()
		{
			IEnumerable<CardModel> enumerable = PileType.Deck.GetPile(base.Owner).Cards.ToList<CardModel>();
			foreach (CardModel cardModel in enumerable)
			{
				if (cardModel.Rarity == CardRarity.Basic && cardModel.Tags.Contains(CardTag.Strike) && ModelDb.Enchantment<TezcatarasEmber>().CanEnchant(cardModel))
				{
					CardCmd.Enchant<TezcatarasEmber>(cardModel, 1m);
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
			return Task.CompletedTask;
		}
	}
}
