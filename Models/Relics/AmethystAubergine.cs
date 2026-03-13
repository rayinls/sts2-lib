using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rewards;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004A9 RID: 1193
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AmethystAubergine : RelicModel
	{
		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x0021031B File Offset: 0x0020E51B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x0021031E File Offset: 0x0020E51E
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x00210326 File Offset: 0x0020E526
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(10));
			}
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00210334 File Offset: 0x0020E534
		public override bool TryModifyRewards(Player player, List<Reward> rewards, [Nullable(2)] AbstractRoom room)
		{
			if (player != base.Owner)
			{
				return false;
			}
			if (room == null)
			{
				return false;
			}
			if (!room.RoomType.IsCombatRoom())
			{
				return false;
			}
			if (room.RoomType == RoomType.Boss && player.RunState.CurrentActIndex >= player.RunState.Acts.Count - 1)
			{
				return false;
			}
			rewards.Add(new GoldReward(base.DynamicVars.Gold.IntValue, player, false));
			return true;
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x002103A8 File Offset: 0x0020E5A8
		public override Task AfterModifyingRewards()
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
