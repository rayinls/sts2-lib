using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200057E RID: 1406
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RingOfTheSnake : RelicModel
	{
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06004EEB RID: 20203 RVA: 0x0021A0B9 File Offset: 0x002182B9
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Starter;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x0021A0BC File Offset: 0x002182BC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0021A0C9 File Offset: 0x002182C9
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
			return count + base.DynamicVars.Cards.BaseValue;
		}
	}
}
