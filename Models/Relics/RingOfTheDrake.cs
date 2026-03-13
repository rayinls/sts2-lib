using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200057D RID: 1405
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RingOfTheDrake : RelicModel
	{
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0021A023 File Offset: 0x00218223
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x0021A026 File Offset: 0x00218226
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new CardsVar(2),
					new DynamicVar("Turns", 3m)
				});
			}
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0021A050 File Offset: 0x00218250
		public override decimal ModifyHandDraw(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			if (player.Creature.CombatState.RoundNumber > base.DynamicVars["Turns"].BaseValue)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.BaseValue;
		}

		// Token: 0x04002210 RID: 8720
		private const string _turnsKey = "Turns";
	}
}
