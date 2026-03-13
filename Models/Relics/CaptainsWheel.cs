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
	// Token: 0x020004CF RID: 1231
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CaptainsWheel : RelicModel
	{
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x06004A8B RID: 19083 RVA: 0x0021230B File Offset: 0x0021050B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x0021230E File Offset: 0x0021050E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(18m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x06004A8D RID: 19085 RVA: 0x00212322 File Offset: 0x00210522
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x00212334 File Offset: 0x00210534
		public override async Task AfterBlockCleared(Creature creature)
		{
			if (creature.CombatState.RoundNumber == 3)
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
