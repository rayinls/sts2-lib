using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000517 RID: 1303
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HornCleat : RelicModel
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06004C2E RID: 19502 RVA: 0x00215240 File Offset: 0x00213440
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x00215243 File Offset: 0x00213443
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(14m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06004C30 RID: 19504 RVA: 0x00215257 File Offset: 0x00213457
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004C31 RID: 19505 RVA: 0x0021526C File Offset: 0x0021346C
		public override async Task AfterBlockCleared(Creature creature)
		{
			if (creature.CombatState.RoundNumber == 2)
			{
				if (creature == base.Owner.Creature)
				{
					base.Flash();
					await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, false);
				}
			}
		}
	}
}
