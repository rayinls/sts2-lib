using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005C5 RID: 1477
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WingCharm : RelicModel
	{
		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x060050AC RID: 20652 RVA: 0x0021D726 File Offset: 0x0021B926
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x060050AD RID: 20653 RVA: 0x0021D729 File Offset: 0x0021B929
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("SwiftAmount", 1m));
			}
		}

		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x060050AE RID: 20654 RVA: 0x0021D73F File Offset: 0x0021B93F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Swift>(base.DynamicVars["SwiftAmount"].IntValue);
			}
		}

		// Token: 0x060050AF RID: 20655 RVA: 0x0021D75C File Offset: 0x0021B95C
		public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			Swift canonicalSwift = ModelDb.Enchantment<Swift>();
			List<CardCreationResult> list = cardRewards.Where((CardCreationResult r) => canonicalSwift.CanEnchant(r.Card)).ToList<CardCreationResult>();
			if (list.Count == 0)
			{
				return false;
			}
			CardCreationResult cardCreationResult = base.Owner.RunState.Rng.Niche.NextItem<CardCreationResult>(list);
			if (cardCreationResult == null)
			{
				return false;
			}
			CardModel cardModel = base.Owner.RunState.CloneCard(cardCreationResult.Card);
			CardCmd.Enchant<Swift>(cardModel, base.DynamicVars["SwiftAmount"].BaseValue);
			cardCreationResult.ModifyCard(cardModel, this);
			return true;
		}

		// Token: 0x04002241 RID: 8769
		private const string _swiftAmountKey = "SwiftAmount";
	}
}
