using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004D4 RID: 1236
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ChemicalX : RelicModel
	{
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00212637 File Offset: 0x00210837
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06004AA9 RID: 19113 RVA: 0x0021263A File Offset: 0x0021083A
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Increase", 2m));
			}
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x00212654 File Offset: 0x00210854
		public override Task BeforeCardPlayed(CardPlay cardPlay)
		{
			if (cardPlay.Card.Owner != base.Owner)
			{
				return Task.CompletedTask;
			}
			if (!cardPlay.Card.EnergyCost.CostsX && !cardPlay.Card.HasStarCostX)
			{
				return Task.CompletedTask;
			}
			base.Flash();
			return Task.CompletedTask;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x002126AA File Offset: 0x002108AA
		public override int ModifyXValue(CardModel card, int originalValue)
		{
			if (base.Owner != card.Owner)
			{
				return originalValue;
			}
			return originalValue + base.DynamicVars["Increase"].IntValue;
		}

		// Token: 0x040021A0 RID: 8608
		private const string _increaseKey = "Increase";
	}
}
