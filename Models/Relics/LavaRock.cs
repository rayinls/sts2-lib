using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000528 RID: 1320
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LavaRock : RelicModel
	{
		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06004CB2 RID: 19634 RVA: 0x002161D0 File Offset: 0x002143D0
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06004CB3 RID: 19635 RVA: 0x002161D3 File Offset: 0x002143D3
		public override bool ShowCounter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06004CB4 RID: 19636 RVA: 0x002161D6 File Offset: 0x002143D6
		// (set) Token: 0x06004CB5 RID: 19637 RVA: 0x002161DE File Offset: 0x002143DE
		[SavedProperty]
		public bool HasTriggered
		{
			get
			{
				return this._hasTriggered;
			}
			set
			{
				base.AssertMutable();
				this._hasTriggered = value;
				base.InvokeDisplayAmountChanged();
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x002161F3 File Offset: 0x002143F3
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Relics", 2m));
			}
		}

		// Token: 0x06004CB7 RID: 19639 RVA: 0x0021620C File Offset: 0x0021440C
		public override bool TryModifyRewards(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (room == null || room.RoomType != RoomType.Boss)
			{
				return false;
			}
			if (base.Owner.RunState.CurrentActIndex != 0)
			{
				return false;
			}
			if (this.HasTriggered)
			{
				return false;
			}
			base.Flash();
			for (int i = 0; i < base.DynamicVars["Relics"].IntValue; i++)
			{
				rewards.Add(new RelicReward(player));
			}
			this.HasTriggered = true;
			base.Status = RelicStatus.Disabled;
			return true;
		}

		// Token: 0x040021D4 RID: 8660
		private const string _relicsKey = "Relics";

		// Token: 0x040021D5 RID: 8661
		private bool _hasTriggered;
	}
}
