using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007BB RID: 1979
	public class Terminal : ModifierModel
	{
		// Token: 0x060060F1 RID: 24817 RVA: 0x00243E48 File Offset: 0x00242048
		[NullableContext(1)]
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			if (base.RunState.BaseRoom == room)
			{
				foreach (Player player in base.RunState.Players)
				{
					await CreatureCmd.LoseMaxHp(new ThrowingPlayerChoiceContext(), player.Creature, 1m, false);
				}
				IEnumerator<Player> enumerator = null;
			}
			CombatRoom combatRoom = room as CombatRoom;
			if (combatRoom != null)
			{
				foreach (Creature creature in combatRoom.CombatState.PlayerCreatures)
				{
					await PowerCmd.Apply<PlatingPower>(creature, 5m, null, null, false);
				}
				IEnumerator<Creature> enumerator2 = null;
			}
		}
	}
}
