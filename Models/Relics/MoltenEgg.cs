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
	// Token: 0x02000540 RID: 1344
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MoltenEgg : RelicModel
	{
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06004D3F RID: 19775 RVA: 0x002170D8 File Offset: 0x002152D8
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x002170DB File Offset: 0x002152DB
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x002170E3 File Offset: 0x002152E3
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
			EggRelicHelper.UpgradeValidCards(cardRewards, CardType.Attack, this);
			return true;
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x00217113 File Offset: 0x00215313
		public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
		{
			if (player != base.Owner)
			{
				return;
			}
			EggRelicHelper.UpgradeValidCards(cards, CardType.Attack, this);
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x00217128 File Offset: 0x00215328
		public override bool TryModifyCardBeingAddedToDeck(CardModel card, [Nullable(2)] out CardModel newCard)
		{
			newCard = null;
			if (card.Owner != base.Owner)
			{
				return false;
			}
			if (card.Type != CardType.Attack)
			{
				return false;
			}
			if (!card.IsUpgradable)
			{
				return false;
			}
			if (card.CurrentUpgradeLevel >= 1)
			{
				return false;
			}
			newCard = base.Owner.RunState.CloneCard(card);
			CardCmd.Upgrade(newCard, CardPreviewStyle.None);
			return true;
		}
	}
}
