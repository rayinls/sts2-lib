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
	// Token: 0x020004AF RID: 1199
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class BagOfMarbles : RelicModel
	{
		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x060049BA RID: 18874 RVA: 0x002109A3 File Offset: 0x0020EBA3
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x060049BB RID: 18875 RVA: 0x002109A6 File Offset: 0x0020EBA6
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VulnerablePower>(1m));
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x060049BC RID: 18876 RVA: 0x002109B7 File Offset: 0x0020EBB7
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VulnerablePower>());
			}
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x002109C4 File Offset: 0x0020EBC4
		public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					await PowerCmd.Apply<VulnerablePower>(combatState.HittableEnemies, base.DynamicVars.Vulnerable.BaseValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}
