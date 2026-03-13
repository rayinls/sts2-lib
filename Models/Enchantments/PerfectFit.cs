using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Players;

namespace MegaCrit.Sts2.Core.Models.Enchantments
{
	// Token: 0x02000870 RID: 2160
	public sealed class PerfectFit : EnchantmentModel
	{
		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x060065E8 RID: 26088 RVA: 0x00252EA8 File Offset: 0x002510A8
		public override bool HasExtraCardText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060065E9 RID: 26089 RVA: 0x00252EAB File Offset: 0x002510AB
		[NullableContext(1)]
		public override void ModifyShuffleOrder(Player player, List<CardModel> cards, bool isInitialShuffle)
		{
			if (isInitialShuffle)
			{
				return;
			}
			if (!cards.Contains(base.Card))
			{
				return;
			}
			cards.Remove(base.Card);
			cards.Insert(0, base.Card);
		}
	}
}
