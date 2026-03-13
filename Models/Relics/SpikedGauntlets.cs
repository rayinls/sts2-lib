using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x02000597 RID: 1431
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class SpikedGauntlets : RelicModel
	{
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06004F86 RID: 20358 RVA: 0x0021B433 File Offset: 0x00219633
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Ancient;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06004F87 RID: 20359 RVA: 0x0021B436 File Offset: 0x00219636
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new EnergyVar(1));
			}
		}

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06004F88 RID: 20360 RVA: 0x0021B443 File Offset: 0x00219643
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.ForEnergy(this));
			}
		}

		// Token: 0x06004F89 RID: 20361 RVA: 0x0021B450 File Offset: 0x00219650
		public override decimal ModifyMaxEnergy(Player player, decimal amount)
		{
			if (player != base.Owner)
			{
				return amount;
			}
			return amount + base.DynamicVars.Energy.IntValue;
		}

		// Token: 0x06004F8A RID: 20362 RVA: 0x0021B478 File Offset: 0x00219678
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			modifiedCost = originalCost;
			if (card.Owner.Creature != base.Owner.Creature)
			{
				return false;
			}
			if (card.Type != CardType.Power)
			{
				return false;
			}
			modifiedCost = originalCost + 1m;
			return true;
		}
	}
}
