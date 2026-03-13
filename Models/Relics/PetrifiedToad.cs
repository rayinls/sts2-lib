using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Potions;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000564 RID: 1380
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PetrifiedToad : RelicModel
	{
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x06004E46 RID: 20038 RVA: 0x00218E8B File Offset: 0x0021708B
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x06004E47 RID: 20039 RVA: 0x00218E8E File Offset: 0x0021708E
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPotion<PotionShapedRock>());
			}
		}

		// Token: 0x06004E48 RID: 20040 RVA: 0x00218E9C File Offset: 0x0021709C
		public override async Task BeforeCombatStartLate()
		{
			base.Flash();
			await PotionCmd.TryToProcure<PotionShapedRock>(base.Owner);
		}
	}
}
