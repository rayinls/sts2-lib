using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Rewards;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000592 RID: 1426
	public sealed class SmallCapsule : RelicModel
	{
		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06004F68 RID: 20328 RVA: 0x0021B10F File Offset: 0x0021930F
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x06004F69 RID: 20329 RVA: 0x0021B114 File Offset: 0x00219314
		[NullableContext(1)]
		public override async Task AfterObtained()
		{
			await RewardsCmd.OfferCustom(base.Owner, new List<Reward>(1)
			{
				new RelicReward(base.Owner)
			});
		}
	}
}
