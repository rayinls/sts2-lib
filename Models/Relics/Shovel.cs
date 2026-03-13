using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200058D RID: 1421
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Shovel : RelicModel
	{
		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06004F3D RID: 20285 RVA: 0x0021AC37 File Offset: 0x00218E37
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Rare;
			}
		}

		// Token: 0x06004F3E RID: 20286 RVA: 0x0021AC3A File Offset: 0x00218E3A
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x06004F3F RID: 20287 RVA: 0x0021AC42 File Offset: 0x00218E42
		public override bool TryModifyRestSiteOptions(Player player, ICollection<RestSiteOption> options)
		{
			if (player != base.Owner)
			{
				return false;
			}
			options.Add(new DigRestSiteOption(player));
			return true;
		}
	}
}
