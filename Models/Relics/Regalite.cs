using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000579 RID: 1401
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Regalite : RelicModel
	{
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06004ED3 RID: 20179 RVA: 0x00219DFB File Offset: 0x00217FFB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06004ED4 RID: 20180 RVA: 0x00219DFE File Offset: 0x00217FFE
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new BlockVar(2m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x00219E14 File Offset: 0x00218014
		public override async Task AfterCardEnteredCombat(CardModel card)
		{
			if (card.Owner == base.Owner)
			{
				if (card.VisualCardPool.IsColorless)
				{
					await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, null, true);
				}
			}
		}
	}
}
