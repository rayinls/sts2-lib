using System;
using System.Collections.Generic;
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
	// Token: 0x02000502 RID: 1282
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class FresnelLens : RelicModel
	{
		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x002140DF File Offset: 0x002122DF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06004BA4 RID: 19364 RVA: 0x002140E2 File Offset: 0x002122E2
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("NimbleAmount", 2m));
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06004BA5 RID: 19365 RVA: 0x002140F9 File Offset: 0x002122F9
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return HoverTipFactory.FromEnchantment<Nimble>(base.DynamicVars["NimbleAmount"].IntValue);
			}
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x00214115 File Offset: 0x00212315
		public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			this.EnchantValidCards(cardRewards);
			return true;
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0021412A File Offset: 0x0021232A
		public override void ModifyMerchantCardCreationResults(Player player, List<CardCreationResult> cards)
		{
			if (player != base.Owner)
			{
				return;
			}
			this.EnchantValidCards(cards);
		}

		// Token: 0x06004BA8 RID: 19368 RVA: 0x0021413D File Offset: 0x0021233D
		public override bool TryModifyCardBeingAddedToDeck(CardModel card, [Nullable(2)] out CardModel newCard)
		{
			newCard = null;
			if (card.Owner != base.Owner)
			{
				return false;
			}
			if (!ModelDb.Enchantment<Nimble>().CanEnchant(card))
			{
				return false;
			}
			newCard = this.EnchantCard(card);
			return true;
		}

		// Token: 0x06004BA9 RID: 19369 RVA: 0x0021416C File Offset: 0x0021236C
		private void EnchantValidCards(List<CardCreationResult> options)
		{
			Nimble nimble = ModelDb.Enchantment<Nimble>();
			foreach (CardCreationResult cardCreationResult in options)
			{
				CardModel card = cardCreationResult.Card;
				if (nimble.CanEnchant(card))
				{
					cardCreationResult.ModifyCard(this.EnchantCard(card), this);
				}
			}
		}

		// Token: 0x06004BAA RID: 19370 RVA: 0x002141D8 File Offset: 0x002123D8
		private CardModel EnchantCard(CardModel card)
		{
			CardModel cardModel = base.Owner.RunState.CloneCard(card);
			CardCmd.Enchant<Nimble>(cardModel, base.DynamicVars["NimbleAmount"].BaseValue);
			return cardModel;
		}

		// Token: 0x040021B3 RID: 8627
		private const string _nimbleAmountKey = "NimbleAmount";
	}
}
