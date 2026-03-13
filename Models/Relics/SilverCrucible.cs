using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000590 RID: 1424
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SilverCrucible : RelicModel
	{
		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06004F54 RID: 20308 RVA: 0x0021AE9F File Offset: 0x0021909F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06004F55 RID: 20309 RVA: 0x0021AEA2 File Offset: 0x002190A2
		public override bool IsUsedUp
		{
			get
			{
				return this.TimesUsed >= base.DynamicVars.Cards.IntValue && this.TreasureRoomsEntered > 0;
			}
		}

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06004F56 RID: 20310 RVA: 0x0021AEC7 File Offset: 0x002190C7
		public override bool ShowCounter
		{
			get
			{
				return base.DynamicVars.Cards.IntValue > 0 && this.TimesUsed < base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06004F57 RID: 20311 RVA: 0x0021AEF6 File Offset: 0x002190F6
		public override int DisplayAmount
		{
			get
			{
				return base.DynamicVars.Cards.IntValue - this.TimesUsed;
			}
		}

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06004F58 RID: 20312 RVA: 0x0021AF0F File Offset: 0x0021910F
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(3));
			}
		}

		// Token: 0x17000FCD RID: 4045
		// (get) Token: 0x06004F59 RID: 20313 RVA: 0x0021AF1C File Offset: 0x0021911C
		// (set) Token: 0x06004F5A RID: 20314 RVA: 0x0021AF24 File Offset: 0x00219124
		[SavedProperty]
		public int TimesUsed
		{
			get
			{
				return this._timesUsed;
			}
			set
			{
				base.AssertMutable();
				this._timesUsed = value;
				base.InvokeDisplayAmountChanged();
				this.CheckIfUsedUp();
			}
		}

		// Token: 0x17000FCE RID: 4046
		// (get) Token: 0x06004F5B RID: 20315 RVA: 0x0021AF3F File Offset: 0x0021913F
		// (set) Token: 0x06004F5C RID: 20316 RVA: 0x0021AF47 File Offset: 0x00219147
		[SavedProperty]
		public int TreasureRoomsEntered
		{
			get
			{
				return this._treasureRoomsEntered;
			}
			set
			{
				base.AssertMutable();
				this._treasureRoomsEntered = value;
				this.CheckIfUsedUp();
			}
		}

		// Token: 0x06004F5D RID: 20317 RVA: 0x0021AF5C File Offset: 0x0021915C
		private void CheckIfUsedUp()
		{
			if (this.IsUsedUp)
			{
				base.Status = RelicStatus.Disabled;
			}
		}

		// Token: 0x06004F5E RID: 20318 RVA: 0x0021AF70 File Offset: 0x00219170
		public override bool TryModifyCardRewardOptionsLate(Player player, List<CardCreationResult> cardRewards, CardCreationOptions options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (this.TimesUsed >= base.DynamicVars.Cards.IntValue)
			{
				return false;
			}
			foreach (CardCreationResult cardCreationResult in cardRewards)
			{
				CardModel card = cardCreationResult.Card;
				if (card.IsUpgradable)
				{
					CardModel cardModel = base.Owner.RunState.CloneCard(card);
					CardCmd.Upgrade(cardModel, CardPreviewStyle.HorizontalLayout);
					cardCreationResult.ModifyCard(cardModel, this);
				}
			}
			return true;
		}

		// Token: 0x06004F5F RID: 20319 RVA: 0x0021B010 File Offset: 0x00219210
		public override Task AfterModifyingCardRewardOptions()
		{
			if (this.TimesUsed >= base.DynamicVars.Cards.IntValue)
			{
				return Task.CompletedTask;
			}
			int timesUsed = this.TimesUsed;
			this.TimesUsed = timesUsed + 1;
			return Task.CompletedTask;
		}

		// Token: 0x06004F60 RID: 20320 RVA: 0x0021B050 File Offset: 0x00219250
		public override Task AfterRoomEntered(AbstractRoom room)
		{
			if (room is TreasureRoom)
			{
				int treasureRoomsEntered = this.TreasureRoomsEntered;
				this.TreasureRoomsEntered = treasureRoomsEntered + 1;
			}
			return Task.CompletedTask;
		}

		// Token: 0x06004F61 RID: 20321 RVA: 0x0021B07A File Offset: 0x0021927A
		public override bool ShouldGenerateTreasure(Player player)
		{
			return player != base.Owner || this.TreasureRoomsEntered > 1;
		}

		// Token: 0x0400221A RID: 8730
		private int _timesUsed;

		// Token: 0x0400221B RID: 8731
		private int _treasureRoomsEntered;
	}
}
