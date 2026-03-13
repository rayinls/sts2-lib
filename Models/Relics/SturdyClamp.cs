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
	// Token: 0x0200059E RID: 1438
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SturdyClamp : RelicModel
	{
		// Token: 0x17000FF2 RID: 4082
		// (get) Token: 0x06004FAE RID: 20398 RVA: 0x0021B94F File Offset: 0x00219B4F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x17000FF3 RID: 4083
		// (get) Token: 0x06004FAF RID: 20399 RVA: 0x0021B952 File Offset: 0x00219B52
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(10m, ValueProp.Unpowered));
			}
		}

		// Token: 0x17000FF4 RID: 4084
		// (get) Token: 0x06004FB0 RID: 20400 RVA: 0x0021B966 File Offset: 0x00219B66
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.Static(StaticHoverTip.Block, Array.Empty<DynamicVar>()));
			}
		}

		// Token: 0x06004FB1 RID: 20401 RVA: 0x0021B978 File Offset: 0x00219B78
		public override bool ShouldClearBlock(Creature creature)
		{
			return creature != base.Owner.Creature;
		}

		// Token: 0x06004FB2 RID: 20402 RVA: 0x0021B98C File Offset: 0x00219B8C
		public override async Task AfterPreventingBlockClear(AbstractModel preventer, Creature creature)
		{
			if (this == preventer)
			{
				if (creature == base.Owner.Creature)
				{
					int block = creature.Block;
					if (block != 0)
					{
						if (block > 10)
						{
							await CreatureCmd.LoseBlock(creature, block - 10);
						}
						base.Flash();
					}
				}
			}
		}

		// Token: 0x04002220 RID: 8736
		private const int _maxRetainedBlock = 10;
	}
}
