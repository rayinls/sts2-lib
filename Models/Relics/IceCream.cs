using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000518 RID: 1304
	public sealed class IceCream : RelicModel
	{
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x002152BF File Offset: 0x002134BF
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004C34 RID: 19508 RVA: 0x002152C2 File Offset: 0x002134C2
		[NullableContext(1)]
		public override bool ShouldPlayerResetEnergy(Player player)
		{
			return player.Creature.CombatState.RoundNumber == 1 || player != base.Owner;
		}
	}
}
