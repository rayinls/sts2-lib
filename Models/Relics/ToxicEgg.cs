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
	// Token: 0x020005AE RID: 1454
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToxicEgg : RelicModel
	{
		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x0600500F RID: 20495 RVA: 0x0021C4C7 File Offset: 0x0021A6C7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0021C4CA File Offset: 0x0021A6CA
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0021C4D2 File Offset: 0x0021A6D2
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
			EggRelicHelper.UpgradeValidCards(cardRewards, CardType.Skill, this);
			return true;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0021C502 File Offset: 0x0021A702
		public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
		{
			if (player != base.Owner)
			{
				return;
			}
			EggRelicHelper.UpgradeValidCards(cards, CardType.Skill, this);
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0021C518 File Offset: 0x0021A718
		public override bool TryModifyCardBeingAddedToDeck(CardModel card, [Nullable(2)] out CardModel newCard)
		{
			newCard = null;
			if (card.Owner != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Skill)
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
