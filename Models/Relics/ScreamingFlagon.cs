using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000587 RID: 1415
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ScreamingFlagon : RelicModel
	{
		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x0021A5DC File Offset: 0x002187DC
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06004F1B RID: 20251 RVA: 0x0021A5DF File Offset: 0x002187DF
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(20m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x0021A5F4 File Offset: 0x002187F4
		public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
		{
			if (side == CombatSide.Player)
			{
				if (PileType.Hand.GetPile(base.Owner).IsEmpty)
				{
					base.Flash();
					await CreatureCmd.Damage(choiceContext, base.Owner.Creature.CombatState.HittableEnemies, base.DynamicVars.Damage, base.Owner.Creature);
				}
			}
		}
	}
}
