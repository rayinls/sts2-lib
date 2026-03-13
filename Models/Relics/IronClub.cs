using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200051B RID: 1307
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IronClub : RelicModel
	{
		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x00215417 File Offset: 0x00213617
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06004C41 RID: 19521 RVA: 0x0021541A File Offset: 0x0021361A
		public override string FlashSfx
		{
			get
			{
				return "event:/sfx/ui/relic_activate_draw";
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06004C42 RID: 19522 RVA: 0x00215421 File Offset: 0x00213621
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x00215424 File Offset: 0x00213624
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.CardsPlayed % base.DynamicVars.Cards.IntValue;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06004C44 RID: 19524 RVA: 0x00215456 File Offset: 0x00213656
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(4));
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x00215463 File Offset: 0x00213663
		// (set) Token: 0x06004C46 RID: 19526 RVA: 0x0021546B File Offset: 0x0021366B
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
				this.UpdateDisplay();
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06004C47 RID: 19527 RVA: 0x00215480 File Offset: 0x00213680
		// (set) Token: 0x06004C48 RID: 19528 RVA: 0x00215488 File Offset: 0x00213688
		[SavedProperty]
		public int CardsPlayed
		{
			get
			{
				return this._cardsPlayed;
			}
			set
			{
				base.AssertMutable();
				this._cardsPlayed = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004C49 RID: 19529 RVA: 0x002154A0 File Offset: 0x002136A0
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars.Cards.IntValue;
				base.Status = ((this.CardsPlayed % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004C4A RID: 19530 RVA: 0x002154EC File Offset: 0x002136EC
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				int cardsPlayed = this.CardsPlayed;
				this.CardsPlayed = cardsPlayed + 1;
				int intValue = base.DynamicVars.Cards.IntValue;
				if (CombatManager.Instance.IsInProgress && this.CardsPlayed % intValue == 0)
				{
					TaskHelper.RunSafely(this.DoActivateVisuals());
					await CardPileCmd.Draw(context, 1m, base.Owner, false);
				}
			}
		}

		// Token: 0x06004C4B RID: 19531 RVA: 0x00215540 File Offset: 0x00213740
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021C4 RID: 8644
		private bool _isActivating;

		// Token: 0x040021C5 RID: 8645
		private int _cardsPlayed;
	}
}
