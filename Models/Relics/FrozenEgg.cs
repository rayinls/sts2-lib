using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Helpers.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000503 RID: 1283
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FrozenEgg : RelicModel
	{
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x0021421C File Offset: 0x0021241C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0021421F File Offset: 0x0021241F
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x00214227 File Offset: 0x00212427
		public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (options.Flags.HasFlag(CardCreationFlags.NoHookUpgrades))
			{
				return false;
			}
			EggRelicHelper.UpgradeValidCards(cardRewards, CardType.Power, this);
			return true;
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x00214257 File Offset: 0x00212457
		public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
		{
			if (player != base.Owner)
			{
				return;
			}
			EggRelicHelper.UpgradeValidCards(cards, CardType.Power, this);
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0021426C File Offset: 0x0021246C
		public override bool TryModifyCardBeingAddedToDeck(CardModel card, [Nullable(2)] out CardModel newCard)
		{
			newCard = null;
			if (card.Owner != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Power)
			{
				return false;
			}
			if (!card.IsUpgradable)
			{
				return false;
			}
			newCard = base.Owner.RunState.CloneCard(card);
			CardCmd.Upgrade(newCard, CardPreviewStyle.None);
			return true;
		}
	}
}
