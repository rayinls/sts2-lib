using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020005C7 RID: 1479
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class WongosMysteryTicket : RelicModel
	{
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x060050B3 RID: 20659 RVA: 0x0021D817 File Offset: 0x0021BA17
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x0021D81A File Offset: 0x0021BA1A
		public override bool IsUsedUp
		{
			get
			{
				return this.GaveRelic;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x0021D822 File Offset: 0x0021BA22
		public override bool ShowCounter
		{
			get
			{
				return this.DisplayAmount > 0;
			}
		}

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x0021D82D File Offset: 0x0021BA2D
		public override int DisplayAmount
		{
			get
			{
				return 5 - this.CombatsFinished;
			}
		}

		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x060050B7 RID: 20663 RVA: 0x0021D837 File Offset: 0x0021BA37
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new RepeatVar(3),
					new DynamicVar("RemainingCombats", 5m)
				});
			}
		}

		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x060050B8 RID: 20664 RVA: 0x0021D860 File Offset: 0x0021BA60
		// (set) Token: 0x060050B9 RID: 20665 RVA: 0x0021D868 File Offset: 0x0021BA68
		[SavedProperty]
		public int CombatsFinished
		{
			get
			{
				return this._combatsFinished;
			}
			set
			{
				base.AssertMutable();
				this._combatsFinished = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x060050BA RID: 20666 RVA: 0x0021D87D File Offset: 0x0021BA7D
		// (set) Token: 0x060050BB RID: 20667 RVA: 0x0021D885 File Offset: 0x0021BA85
		[SavedProperty]
		public bool GaveRelic
		{
			get
			{
				return this._gaveRelic;
			}
			set
			{
				base.AssertMutable();
				this._gaveRelic = value;
				base.InvokeDisplayAmountChanged();
				if (this._gaveRelic)
				{
					base.Status = RelicStatus.Disabled;
				}
			}
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x0021D8AC File Offset: 0x0021BAAC
		public override Task AfterCombatEnd(CombatRoom _)
		{
			int combatsFinished = this.CombatsFinished;
			this.CombatsFinished = combatsFinished + 1;
			int num = 5 - this.CombatsFinished;
			base.DynamicVars["RemainingCombats"].BaseValue = decimal.Max(num, 0m);
			return Task.CompletedTask;
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x0021D8FC File Offset: 0x0021BAFC
		public override bool TryModifyRewards(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (!(room is CombatRoom))
			{
				return false;
			}
			if (this.GaveRelic)
			{
				return false;
			}
			int num = 5 - this.CombatsFinished;
			if (num > 0)
			{
				return false;
			}
			for (int i = 0; i < base.DynamicVars.Repeat.IntValue; i++)
			{
				rewards.Add(new RelicReward(player));
			}
			return true;
		}

		// Token: 0x060050BE RID: 20670 RVA: 0x0021D95F File Offset: 0x0021BB5F
		public override Task AfterModifyingRewards()
		{
			base.Flash();
			this.GaveRelic = true;
			return Task.CompletedTask;
		}

		// Token: 0x04002242 RID: 8770
		private const string _remainingCombatsKey = "RemainingCombats";

		// Token: 0x04002243 RID: 8771
		public const int combatsToActivate = 5;

		// Token: 0x04002244 RID: 8772
		public const int relicCount = 3;

		// Token: 0x04002245 RID: 8773
		private int _combatsFinished;

		// Token: 0x04002246 RID: 8774
		private bool _gaveRelic;
	}
}
