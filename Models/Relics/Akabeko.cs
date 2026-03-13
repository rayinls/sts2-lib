using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004A7 RID: 1191
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Akabeko : RelicModel
	{
		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x0600497F RID: 18815 RVA: 0x00210235 File Offset: 0x0020E435
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06004980 RID: 18816 RVA: 0x00210238 File Offset: 0x0020E438
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VigorPower>(8m));
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06004981 RID: 18817 RVA: 0x0021024A File Offset: 0x0020E44A
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VigorPower>());
			}
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x00210258 File Offset: 0x0020E458
		public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
		{
			if (side == base.Owner.Creature.Side)
			{
				if (combatState.RoundNumber <= 1)
				{
					base.Flash();
					await PowerCmd.Apply<VigorPower>(base.Owner.Creature, base.DynamicVars["VigorPower"].IntValue, base.Owner.Creature, null, false);
				}
			}
		}
	}
}
