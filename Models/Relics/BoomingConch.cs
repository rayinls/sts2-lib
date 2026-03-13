using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C3 RID: 1219
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BoomingConch : RelicModel
	{
		// Token: 0x17000D52 RID: 3410
		// (get) Token: 0x06004A37 RID: 18999 RVA: 0x0021189B File Offset: 0x0020FA9B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000D53 RID: 3411
		// (get) Token: 0x06004A38 RID: 19000 RVA: 0x0021189E File Offset: 0x0020FA9E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x002118AC File Offset: 0x0020FAAC
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			if (player.Creature.CombatState.RoundNumber > 1)
			{
				return count;
			}
			AbstractRoom currentRoom = player.RunState.CurrentRoom;
			if (currentRoom == null || currentRoom.RoomType != RoomType.Elite)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.IntValue;
		}
	}
}
