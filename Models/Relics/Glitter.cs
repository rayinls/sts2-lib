using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200050C RID: 1292
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Glitter : RelicModel
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06004BEF RID: 19439 RVA: 0x00214B6F File Offset: 0x00212D6F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x00214B72 File Offset: 0x00212D72
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Glam>(1);
			}
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x00214B7C File Offset: 0x00212D7C
		public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			Glam glam = ModelDb.Enchantment<Glam>();
			foreach (CardCreationResult cardCreationResult in cardRewards)
			{
				CardModel card = cardCreationResult.Card;
				if (glam.CanEnchant(card))
				{
					CardModel cardModel = base.Owner.RunState.CloneCard(card);
					CardCmd.Enchant<Glam>(cardModel, 1m);
					cardCreationResult.ModifyCard(cardModel, this);
				}
			}
			return true;
		}
	}
}
