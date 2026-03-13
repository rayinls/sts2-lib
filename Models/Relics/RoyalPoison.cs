using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000580 RID: 1408
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RoyalPoison : RelicModel
	{
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0021A24A File Offset: 0x0021844A
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06004EF9 RID: 20217 RVA: 0x0021A24D File Offset: 0x0021844D
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(4m, ValueProp.Unblockable | ValueProp.Unpowered));
			}
		}

		// Token: 0x06004EFA RID: 20218 RVA: 0x0021A260 File Offset: 0x00218460
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (player.Creature.CombatState.RoundNumber <= 1)
				{
					base.Flash();
					await CreatureCmd.Damage(choiceContext, base.Owner.Creature, base.DynamicVars.Damage, null, null);
				}
			}
		}
	}
}
