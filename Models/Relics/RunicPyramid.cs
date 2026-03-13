using System;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000584 RID: 1412
	public sealed class RunicPyramid : RelicModel
	{
		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06004F0E RID: 20238 RVA: 0x0021A467 File Offset: 0x00218667
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x0021A46A File Offset: 0x0021866A
		[NullableContext(1)]
		public override bool ShouldFlush(Player player)
		{
			return player != base.Owner;
		}
	}
}
