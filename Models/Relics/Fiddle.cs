using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004FF RID: 1279
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Fiddle : RelicModel
	{
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06004B94 RID: 19348 RVA: 0x00213F77 File Offset: 0x00212177
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06004B95 RID: 19349 RVA: 0x00213F7A File Offset: 0x0021217A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new CardsVar(2));
			}
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x00213F87 File Offset: 0x00212187
		public override decimal ModifyHandDrawLate(Player player, decimal count)
		{
			if (player != base.Owner)
			{
				return count;
			}
			return count + base.DynamicVars.Cards.IntValue;
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x00213FAF File Offset: 0x002121AF
		public override bool ShouldDraw(Player player, bool fromHandDraw)
		{
			return fromHandDraw || player != base.Owner || player.Creature.Side != player.Creature.CombatState.CurrentSide;
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x00213FE1 File Offset: 0x002121E1
		public override Task AfterPreventingDraw()
		{
			base.Flash();
			return Task.CompletedTask;
		}
	}
}
