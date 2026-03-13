using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000541 RID: 1345
	public sealed class MrStruggles : RelicModel
	{
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06004D45 RID: 19781 RVA: 0x0021718C File Offset: 0x0021538C
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x06004D46 RID: 19782 RVA: 0x00217190 File Offset: 0x00215390
		[NullableContext(1)]
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				base.Flash();
				CombatState combatState = player.Creature.CombatState;
				await CreatureCmd.Damage(choiceContext, combatState.HittableEnemies, combatState.RoundNumber, ValueProp.Unpowered, base.Owner.Creature);
			}
		}
	}
}
