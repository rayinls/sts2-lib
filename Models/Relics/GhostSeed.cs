using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000509 RID: 1289
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class GhostSeed : RelicModel
	{
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06004BDC RID: 19420 RVA: 0x0021496F File Offset: 0x00212B6F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x00214972 File Offset: 0x00212B72
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Ethereal));
			}
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0021497F File Offset: 0x00212B7F
		public override Task AfterCardEnteredCombat(CardModel card)
		{
			if (!this.CanAffect(card))
			{
				return Task.CompletedTask;
			}
			CardCmd.ApplyKeyword(card, new CardKeyword[] { CardKeyword.Ethereal });
			return Task.CompletedTask;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x002149A8 File Offset: 0x00212BA8
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			IEnumerable<CardModel> allCards = base.Owner.PlayerCombatState.AllCards;
			foreach (CardModel cardModel in allCards)
			{
				if (this.CanAffect(cardModel))
				{
					CardCmd.ApplyKeyword(cardModel, new CardKeyword[] { CardKeyword.Ethereal });
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x00214A28 File Offset: 0x00212C28
		public bool CanAffect(CardModel card)
		{
			return card.Rarity == CardRarity.Basic && (card.Tags.Contains(CardTag.Strike) || card.Tags.Contains(CardTag.Defend)) && !card.Keywords.Contains(CardKeyword.Ethereal);
		}
	}
}
