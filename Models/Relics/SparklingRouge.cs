using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000596 RID: 1430
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SparklingRouge : RelicModel
	{
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06004F81 RID: 20353 RVA: 0x0021B396 File Offset: 0x00219596
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06004F82 RID: 20354 RVA: 0x0021B399 File Offset: 0x00219599
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlyArray<DynamicVar>(new DynamicVar[]
				{
					new PowerVar<StrengthPower>(1m),
					new PowerVar<DexterityPower>(1m)
				});
			}
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06004F83 RID: 20355 RVA: 0x0021B3C0 File Offset: 0x002195C0
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlyArray<IHoverTip>(new IHoverTip[]
				{
					HoverTipFactory.FromPower<StrengthPower>(),
					HoverTipFactory.FromPower<DexterityPower>()
				});
			}
		}

		// Token: 0x06004F84 RID: 20356 RVA: 0x0021B3E0 File Offset: 0x002195E0
		public override async Task AfterBlockCleared(Creature creature)
		{
			if (creature.CombatState.RoundNumber == 3)
			{
				if (creature == base.Owner.Creature)
				{
					base.Flash();
					await PowerCmd.Apply<StrengthPower>(base.Owner.Creature, base.DynamicVars.Strength.BaseValue, base.Owner.Creature, null, false);
					await PowerCmd.Apply<DexterityPower>(base.Owner.Creature, base.DynamicVars.Dexterity.BaseValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}
