using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Runs;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000532 RID: 1330
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class LuckyFysh : RelicModel
	{
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x06004CEF RID: 19695 RVA: 0x00216863 File Offset: 0x00214A63
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Uncommon;
			}
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x00216866 File Offset: 0x00214A66
		public override bool IsAllowed(IRunState runState)
		{
			return RelicModel.IsBeforeAct3TreasureChest(runState);
		}

		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x06004CF1 RID: 19697 RVA: 0x0021686E File Offset: 0x00214A6E
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new GoldVar(15));
			}
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0021687C File Offset: 0x00214A7C
		public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			CardPile pile = card.Pile;
			if (pile != null && pile.Type == PileType.Deck)
			{
				if (card.Owner == base.Owner)
				{
					base.Flash();
					await PlayerCmd.GainGold(base.DynamicVars.Gold.BaseValue, base.Owner, false);
				}
			}
		}
	}
}
