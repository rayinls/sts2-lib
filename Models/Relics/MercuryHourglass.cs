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
	// Token: 0x0200053B RID: 1339
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MercuryHourglass : RelicModel
	{
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06004D1D RID: 19741 RVA: 0x00216D06 File Offset: 0x00214F06
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06004D1E RID: 19742 RVA: 0x00216D09 File Offset: 0x00214F09
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(3m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004D1F RID: 19743 RVA: 0x00216D1C File Offset: 0x00214F1C
		public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				base.Flash();
				await CreatureCmd.Damage(choiceContext, player.Creature.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature);
			}
		}
	}
}
