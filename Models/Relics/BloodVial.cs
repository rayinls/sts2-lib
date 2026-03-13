using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004BD RID: 1213
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BloodVial : RelicModel
	{
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06004A0F RID: 18959 RVA: 0x00211386 File Offset: 0x0020F586
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Common;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06004A10 RID: 18960 RVA: 0x00211389 File Offset: 0x0020F589
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new HealVar(2m));
			}
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x0021139C File Offset: 0x0020F59C
		public override async Task AfterPlayerTurnStartLate(PlayerChoiceContext choiceContext, Player player)
		{
			if (player == base.Owner)
			{
				if (player.Creature.CombatState.RoundNumber <= 1)
				{
					await CreatureCmd.Heal(base.Owner.Creature, base.DynamicVars.Heal.IntValue, true);
				}
			}
		}
	}
}
