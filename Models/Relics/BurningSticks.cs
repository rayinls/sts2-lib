using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004CB RID: 1227
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BurningSticks : RelicModel
	{
		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06004A6B RID: 19051 RVA: 0x00211F47 File Offset: 0x00210147
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06004A6C RID: 19052 RVA: 0x00211F4A File Offset: 0x0021014A
		// (set) Token: 0x06004A6D RID: 19053 RVA: 0x00211F52 File Offset: 0x00210152
		private bool WasUsedThisCombat
		{
			get
			{
				return this._wasUsedThisCombat;
			}
			set
			{
				base.AssertMutable();
				this._wasUsedThisCombat = value;
			}
		}

		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x06004A6E RID: 19054 RVA: 0x00211F61 File Offset: 0x00210161
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x00211F6E File Offset: 0x0021016E
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (!(room is CombatRoom))
			{
				return Task.CompletedTask;
			}
			this.WasUsedThisCombat = false;
			base.Status = RelicStatus.Active;
			return Task.CompletedTask;
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x00211F94 File Offset: 0x00210194
		public override async Task AfterCardExhausted(PlayerChoiceContext choiceContext, CardModel card, bool causedByEthereal)
		{
			if (card.Owner == base.Owner)
			{
				if (!this.WasUsedThisCombat)
				{
					if (card.Type == CardType.Skill)
					{
						base.Flash();
						CardModel cardModel = card.CreateClone();
						await CardPileCmd.AddGeneratedCardToCombat(cardModel, PileType.Hand, true, CardPilePosition.Bottom);
						base.Status = RelicStatus.Normal;
						this.WasUsedThisCombat = true;
					}
				}
			}
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x00211FDF File Offset: 0x002101DF
		public override Task AfterCombatEnd(CombatRoom _)
		{
			this.WasUsedThisCombat = false;
			base.Status = RelicStatus.Normal;
			return Task.CompletedTask;
		}

		// Token: 0x04002198 RID: 8600
		private bool _wasUsedThisCombat;
	}
}
