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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000548 RID: 1352
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Nunchaku : RelicModel
	{
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06004D6A RID: 19818 RVA: 0x002175D3 File Offset: 0x002157D3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06004D6B RID: 19819 RVA: 0x002175D6 File Offset: 0x002157D6
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06004D6C RID: 19820 RVA: 0x002175D9 File Offset: 0x002157D9
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.AttacksPlayed % base.DynamicVars.Cards.IntValue;
				}
				return base.DynamicVars.Cards.IntValue;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06004D6D RID: 19821 RVA: 0x0021760B File Offset: 0x0021580B
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(10),
					new EnergyVar(1)
				});
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06004D6E RID: 19822 RVA: 0x0021762B File Offset: 0x0021582B
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06004D6F RID: 19823 RVA: 0x00217638 File Offset: 0x00215838
		// (set) Token: 0x06004D70 RID: 19824 RVA: 0x00217640 File Offset: 0x00215840
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

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06004D71 RID: 19825 RVA: 0x00217655 File Offset: 0x00215855
		// (set) Token: 0x06004D72 RID: 19826 RVA: 0x0021765D File Offset: 0x0021585D
		[SavedProperty]
		public int AttacksPlayed
		{
			get
			{
				return this._attacksPlayed;
			}
			set
			{
				base.AssertMutable();
				this._attacksPlayed = value;
				this.UpdateDisplay();
			}
		}

		// Token: 0x06004D73 RID: 19827 RVA: 0x00217674 File Offset: 0x00215874
		private void UpdateDisplay()
		{
			if (this.IsActivating)
			{
				base.Status = RelicStatus.Normal;
			}
			else
			{
				int intValue = base.DynamicVars.Cards.IntValue;
				base.Status = ((this.AttacksPlayed % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
			}
			base.InvokeDisplayAmountChanged();
		}

		// Token: 0x06004D74 RID: 19828 RVA: 0x002176C0 File Offset: 0x002158C0
		public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner == base.Owner)
			{
				if (cardPlay.Card.Type == CardType.Attack)
				{
					int attacksPlayed = this.AttacksPlayed;
					this.AttacksPlayed = attacksPlayed + 1;
					int intValue = base.DynamicVars.Cards.IntValue;
					if (CombatManager.Instance.IsInProgress && this.AttacksPlayed % intValue == 0)
					{
						TaskHelper.RunSafely(this.DoActivateVisuals());
						await PlayerCmd.GainEnergy(base.DynamicVars.Energy.BaseValue, base.Owner);
					}
				}
			}
		}

		// Token: 0x06004D75 RID: 19829 RVA: 0x0021770C File Offset: 0x0021590C
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021E4 RID: 8676
		private bool _isActivating;

		// Token: 0x040021E5 RID: 8677
		private int _attacksPlayed;
	}
}
