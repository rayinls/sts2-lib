using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000577 RID: 1399
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RedMask : RelicModel
	{
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x00219C3B File Offset: 0x00217E3B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x00219C3E File Offset: 0x00217E3E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<WeakPower>(1m));
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x00219C4F File Offset: 0x00217E4F
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<WeakPower>());
			}
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00219C5C File Offset: 0x00217E5C
		public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					await PowerCmd.Apply<WeakPower>(combatState.HittableEnemies, base.DynamicVars["WeakPower"].BaseValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}
