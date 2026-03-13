using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D8 RID: 1240
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Claws : RelicModel
	{
		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x002127C1 File Offset: 0x002109C1
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x002127C4 File Offset: 0x002109C4
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(6));
			}
		}

		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x002127D1 File Offset: 0x002109D1
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromCardWithCardHoverTips<Maul>(false);
			}
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x002127DC File Offset: 0x002109DC
		public override async Task AfterObtained()
		{
			CardSelectorPrefs cardSelectorPrefs = new CardSelectorPrefs(base.SelectionScreenPrompt, 0, base.DynamicVars.Cards.IntValue)
			{
				Cancelable = false,
				RequireManualConfirmation = true
			};
			IEnumerable<CardModel> enumerable = await CardSelectCmd.FromDeckForTransformation(base.Owner, cardSelectorPrefs, (CardModel c) => new CardTransformation(c, this.CreateMaulFromOriginal(c, true)));
			IEnumerable<CardModel> enumerable2 = enumerable;
			List<CardTransformation> list = enumerable2.Select((CardModel original) => new CardTransformation(original, this.CreateMaulFromOriginal(original, false))).ToList<CardTransformation>();
			await CardCmd.Transform(list, base.Owner.PlayerRng.Transformations, CardPreviewStyle.HorizontalLayout);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00212820 File Offset: 0x00210A20
		private CardModel CreateMaulFromOriginal(CardModel original, bool forPreview)
		{
			CardModel cardModel = (forPreview ? ModelDb.Card<Maul>().ToMutable() : base.Owner.RunState.CreateCard<Maul>(base.Owner));
			if (original.IsUpgraded && cardModel.IsUpgradable)
			{
				if (forPreview)
				{
					cardModel.UpgradeInternal();
				}
				else
				{
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
				}
			}
			if (original.Enchantment != null)
			{
				EnchantmentModel enchantmentModel = (EnchantmentModel)original.Enchantment.MutableClone();
				if (enchantmentModel.CanEnchant(cardModel))
				{
					if (forPreview)
					{
						cardModel.EnchantInternal(enchantmentModel, enchantmentModel.Amount);
						enchantmentModel.ModifyCard();
					}
					else
					{
						CardCmd.Enchant(enchantmentModel, cardModel, enchantmentModel.Amount);
					}
				}
			}
			return cardModel;
		}
	}
}
