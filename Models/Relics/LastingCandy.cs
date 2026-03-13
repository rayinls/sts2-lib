using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000526 RID: 1318
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LastingCandy : RelicModel
	{
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06004C9E RID: 19614 RVA: 0x00215EE1 File Offset: 0x002140E1
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004C9F RID: 19615 RVA: 0x00215EE4 File Offset: 0x002140E4
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06004CA0 RID: 19616 RVA: 0x00215EEC File Offset: 0x002140EC
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06004CA1 RID: 19617 RVA: 0x00215EEF File Offset: 0x002140EF
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.CombatsSeen % 2;
				}
				return 2;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06004CA2 RID: 19618 RVA: 0x00215F03 File Offset: 0x00214103
		// (set) Token: 0x06004CA3 RID: 19619 RVA: 0x00215F0B File Offset: 0x0021410B
		private bool IsActivating
		{
			get
			{
				return this._isActivating;
			}
			set
			{
				base.AssertMutable();
				this._isActivating = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06004CA4 RID: 19620 RVA: 0x00215F20 File Offset: 0x00214120
		// (set) Token: 0x06004CA5 RID: 19621 RVA: 0x00215F28 File Offset: 0x00214128
		[SavedProperty]
		public int CombatsSeen
		{
			get
			{
				return this._combatsSeen;
			}
			set
			{
				base.AssertMutable();
				this._combatsSeen = value;
			}
		}

		// Token: 0x06004CA6 RID: 19622 RVA: 0x00215F38 File Offset: 0x00214138
		public override bool TryModifyCardRewardOptions(Player player, List<CardCreationResult> options, CardCreationOptions creationOptions)
		{
			if (base.Owner != player)
			{
				return false;
			}
			if (creationOptions.Source != CardCreationSource.Encounter)
			{
				return false;
			}
			if (!this.IsInTriggeringCombat)
			{
				return false;
			}
			IEnumerable<CardModel> enumerable = from c in creationOptions.GetPossibleCards(player)
				where c.Type == CardType.Power && options.TrueForAll((CardCreationResult o) => o.originalCard.Id != c.Id)
				select c;
			CardCreationOptions cardCreationOptions = new CardCreationOptions(enumerable, CardCreationSource.Other, creationOptions.RarityOdds).WithFlags(CardCreationFlags.NoModifyHooks | CardCreationFlags.NoCardPoolModifications);
			CardCreationResult cardCreationResult = CardFactory.CreateForReward(base.Owner, 1, cardCreationOptions).FirstOrDefault<CardCreationResult>();
			CardModel cardModel = ((cardCreationResult != null) ? cardCreationResult.Card : null);
			if (cardModel != null)
			{
				CardCreationResult cardCreationResult2 = new CardCreationResult(cardModel);
				cardCreationResult2.ModifyCard(cardModel, this);
				options.Add(cardCreationResult2);
			}
			return cardModel != null;
		}

		// Token: 0x06004CA7 RID: 19623 RVA: 0x00215FE4 File Offset: 0x002141E4
		public override Task AfterCombatEnd(CombatRoom room)
		{
			int combatsSeen = this.CombatsSeen;
			this.CombatsSeen = combatsSeen + 1;
			if (this.IsInTriggeringCombat)
			{
				TaskHelper.RunSafely(this.DoActivateVisuals());
			}
			base.InvokeDisplayAmountChanged();
			return Task.CompletedTask;
		}

		// Token: 0x06004CA8 RID: 19624 RVA: 0x00216020 File Offset: 0x00214220
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06004CA9 RID: 19625 RVA: 0x00216063 File Offset: 0x00214263
		private bool IsInTriggeringCombat
		{
			get
			{
				return this.CombatsSeen > 0 && this.CombatsSeen % 2 == 0;
			}
		}

		// Token: 0x040021D1 RID: 8657
		private bool _isActivating;

		// Token: 0x040021D2 RID: 8658
		private int _combatsSeen;
	}
}
