using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x020004DD RID: 1245
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DarkstonePeriapt : RelicModel
	{
		// Token: 0x17000D9F RID: 3487
		// (get) Token: 0x06004AD3 RID: 19155 RVA: 0x00212AC7 File Offset: 0x00210CC7
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Event;
			}
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06004AD4 RID: 19156 RVA: 0x00212ACA File Offset: 0x00210CCA
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new MaxHpVar(6m));
			}
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00212ADC File Offset: 0x00210CDC
		public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, [Nullable(2)] AbstractModel source)
		{
			CardPile pile = card.Pile;
			if (pile != null && pile.Type == PileType.Deck)
			{
				if (card.Owner == base.Owner)
				{
					if (card.Type == CardType.Curse)
					{
						base.Flash();
						await CreatureCmd.GainMaxHp(base.Owner.Creature, base.DynamicVars.MaxHp.BaseValue);
					}
				}
			}
		}
	}
}
