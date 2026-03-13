using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005AF RID: 1455
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ToyBox : RelicModel
	{
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06005015 RID: 20501 RVA: 0x0021C571 File Offset: 0x0021A771
		public static LocString WaxRelicPrefix
		{
			get
			{
				return new LocString("relics", "TOY_BOX.waxRelicPrefix");
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06005016 RID: 20502 RVA: 0x0021C582 File Offset: 0x0021A782
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x0021C585 File Offset: 0x0021A785
		public override bool HasUponPickupEffect
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x0021C588 File Offset: 0x0021A788
		public override bool IsUsedUp
		{
			get
			{
				return this.CombatsSeen >= base.DynamicVars["Combats"].IntValue * base.DynamicVars["Relics"].IntValue;
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x0021C5C0 File Offset: 0x0021A7C0
		public override bool ShowCounter
		{
			get
			{
				return !this.IsUsedUp;
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x0600501A RID: 20506 RVA: 0x0021C5CB File Offset: 0x0021A7CB
		public override int DisplayAmount
		{
			get
			{
				if (!this.IsActivating)
				{
					return this.CombatsSeen % base.DynamicVars["Combats"].IntValue;
				}
				return base.DynamicVars["Combats"].IntValue;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x0600501B RID: 20507 RVA: 0x0021C607 File Offset: 0x0021A807
		// (set) Token: 0x0600501C RID: 20508 RVA: 0x0021C60F File Offset: 0x0021A80F
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

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x0600501D RID: 20509 RVA: 0x0021C624 File Offset: 0x0021A824
		// (set) Token: 0x0600501E RID: 20510 RVA: 0x0021C62C File Offset: 0x0021A82C
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
				base.InvokeDisplayAmountChanged();
				if (this.IsUsedUp)
				{
					base.Status = RelicStatus.Disabled;
				}
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x0600501F RID: 20511 RVA: 0x0021C650 File Offset: 0x0021A850
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new DynamicVar("Relics", 4m),
					new DynamicVar("Combats", 3m)
				});
			}
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x0021C684 File Offset: 0x0021A884
		public override async Task AfterObtained()
		{
			List<Reward> list = new List<Reward>();
			for (int i = 0; i < base.DynamicVars["Relics"].IntValue; i++)
			{
				RelicModel relicModel = RelicFactory.PullNextRelicFromFront(base.Owner).ToMutable();
				relicModel.IsWax = true;
				list.Add(new RelicReward(relicModel, base.Owner));
			}
			await RewardsCmd.OfferCustom(base.Owner, list);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0021C6C8 File Offset: 0x0021A8C8
		public override async Task AfterCombatEnd(CombatRoom __)
		{
			int combatsSeen = this.CombatsSeen;
			this.CombatsSeen = combatsSeen + 1;
			if (this.CombatsSeen % base.DynamicVars["Combats"].IntValue == 0)
			{
				TaskHelper.RunSafely(this.DoActivateVisuals());
				RelicModel relicModel = base.Owner.Relics.FirstOrDefault((RelicModel r) => r != null && r.IsWax && !r.IsMelted);
				if (relicModel != null)
				{
					await RelicCmd.Melt(relicModel);
					await Cmd.CustomScaledWait(0.5f, 0.75f, false, default(CancellationToken));
				}
			}
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x0021C70C File Offset: 0x0021A90C
		private async Task DoActivateVisuals()
		{
			this.IsActivating = true;
			base.Flash();
			await Cmd.Wait(1f, false);
			this.IsActivating = false;
		}

		// Token: 0x04002231 RID: 8753
		private const string _relicsKey = "Relics";

		// Token: 0x04002232 RID: 8754
		private const string _combatsKey = "Combats";

		// Token: 0x04002233 RID: 8755
		private bool _isActivating;

		// Token: 0x04002234 RID: 8756
		private int _combatsSeen;
	}
}
