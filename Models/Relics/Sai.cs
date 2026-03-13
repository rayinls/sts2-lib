using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000585 RID: 1413
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Sai : RelicModel
	{
		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06004F11 RID: 20241 RVA: 0x0021A480 File Offset: 0x00218680
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06004F12 RID: 20242 RVA: 0x0021A483 File Offset: 0x00218683
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(7m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004F13 RID: 20243 RVA: 0x0021A498 File Offset: 0x00218698
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == CombatSide.Player)
			{
				base.Flash();
				await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
			}
		}
	}
}
