using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004C8 RID: 1224
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Brimstone : RelicModel
	{
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x00211DD4 File Offset: 0x0020FFD4
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x00211DD7 File Offset: 0x0020FFD7
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>("SelfStrength", 2m),
					new PowerVar<StrengthPower>("EnemyStrength", 1m)
				});
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x00211E09 File Offset: 0x00210009
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<StrengthPower>());
			}
		}

		// Token: 0x06004A60 RID: 19040 RVA: 0x00211E18 File Offset: 0x00210018
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				base.Flash();
				await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars["SelfStrength"].BaseValue, base.Owner.Creature, null, false);
				IEnumerable<Creature> enumerable = from c in combatState.GetOpponentsOf(base.Owner.Creature)
					where c.IsAlive
					select c;
				await PowerCmd.Apply<StrengthPower>(enumerable, base.DynamicVars["EnemyStrength"].BaseValue, null, null, false);
			}
		}

		// Token: 0x04002196 RID: 8598
		private const string _selfStrengthKey = "SelfStrength";

		// Token: 0x04002197 RID: 8599
		private const string _enemyStrengthKey = "EnemyStrength";
	}
}
