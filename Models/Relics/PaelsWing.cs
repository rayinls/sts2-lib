using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.CardRewardAlternatives;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.Rewards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200055A RID: 1370
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PaelsWing : RelicModel
	{
		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06004E04 RID: 19972 RVA: 0x00218753 File Offset: 0x00216953
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x00218756 File Offset: 0x00216956
		public override bool ShowCounter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06004E06 RID: 19974 RVA: 0x00218759 File Offset: 0x00216959
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.RewardsSacrificed % base.DynamicVars["Sacrifices"].IntValue;
				}
				return base.DynamicVars["Sacrifices"].IntValue;
			}
		}

		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x00218795 File Offset: 0x00216995
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Sacrifices", 2m));
			}
		}

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06004E08 RID: 19976 RVA: 0x002187AC File Offset: 0x002169AC
		// (set) Token: 0x06004E09 RID: 19977 RVA: 0x002187B4 File Offset: 0x002169B4
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

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06004E0A RID: 19978 RVA: 0x002187C9 File Offset: 0x002169C9
		// (set) Token: 0x06004E0B RID: 19979 RVA: 0x002187D1 File Offset: 0x002169D1
		[SavedProperty]
		public int RewardsSacrificed
		{
			get
			{
				return this._rewardsSacrificed;
			}
			set
			{
				base.AssertMutable();
				this._rewardsSacrificed = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x06004E0C RID: 19980 RVA: 0x002187E6 File Offset: 0x002169E6
		public override bool TryModifyCardRewardAlternatives(Player player, CardReward cardReward, List<CardRewardAlternative> alternatives)
		{
			if (base.Owner != player)
			{
				return false;
			}
			alternatives.Add(new CardRewardAlternative("SACRIFICE", new Func<Task>(this.OnSacrificeSynchronized), PostAlternateCardRewardAction.DismissScreenAndRemoveReward));
			return true;
		}

		// Token: 0x06004E0D RID: 19981 RVA: 0x00218814 File Offset: 0x00216A14
		private async Task OnSacrificeSynchronized()
		{
			RunManager.Instance.RewardSynchronizer.SyncLocalPaelsWingSacrifice(this);
			await this.OnSacrifice();
		}

		// Token: 0x06004E0E RID: 19982 RVA: 0x00218858 File Offset: 0x00216A58
		public async Task OnSacrifice()
		{
			int rewardsSacrificed = this.RewardsSacrificed;
			this.RewardsSacrificed = rewardsSacrificed + 1;
			base.Flash();
			if (this.RewardsSacrificed % base.DynamicVars["Sacrifices"].IntValue == 0)
			{
				TaskHelper.RunSafely(this.DoActivateVisuals());
				await RelicCmd.Obtain(RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable(), base.Owner, -1);
			}
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x0021889C File Offset: 0x00216A9C
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x040021F6 RID: 8694
		public const string sacrificeAlternativeKey = "SACRIFICE";

		// Token: 0x040021F7 RID: 8695
		private const string _sacrificesKey = "Sacrifices";

		// Token: 0x040021F8 RID: 8696
		private bool _isActivating;

		// Token: 0x040021F9 RID: 8697
		private int _rewardsSacrificed;
	}
}
