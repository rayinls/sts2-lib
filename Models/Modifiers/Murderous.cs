using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;

namespace MegaCrit.Sts2.Core.Models.Modifiers
{
	// Token: 0x020007B7 RID: 1975
	[NullableContext(1)]
	[Nullable(0)]
	public class Murderous : ModifierModel
	{
		// Token: 0x060060E3 RID: 24803 RVA: 0x00243BF0 File Offset: 0x00241DF0
		public override async Task AfterRoomEntered(AbstractRoom room)
		{
			CombatRoom combatRoom = room as CombatRoom;
			if (combatRoom != null)
			{
				await PowerCmd.Apply<StrengthPower>(combatRoom.CombatState.Creatures, 3m, null, null, false);
			}
		}

		// Token: 0x060060E4 RID: 24804 RVA: 0x00243C33 File Offset: 0x00241E33
		public override Task AfterCreatureAddedToCombat(Creature creature)
		{
			if (creature.Side == CombatSide.Player)
			{
				return Task.CompletedTask;
			}
			return PowerCmd.Apply<StrengthPower>(creature, 3m, null, null, false);
		}

		// Token: 0x04002468 RID: 9320
		private const int _strengthAmount = 3;
	}
}
