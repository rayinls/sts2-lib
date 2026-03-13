using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Powers
{
	// Token: 0x020005F7 RID: 1527
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class CorruptionPower : PowerModel
	{
		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x060051B7 RID: 20919 RVA: 0x0021FD8F File Offset: 0x0021DF8F
		public override PowerType Type
		{
			get
			{
				return PowerType.Buff;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x060051B8 RID: 20920 RVA: 0x0021FD92 File Offset: 0x0021DF92
		public override PowerStackType StackType
		{
			get
			{
				return PowerStackType.Single;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x060051B9 RID: 20921 RVA: 0x0021FD95 File Offset: 0x0021DF95
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Exhaust));
			}
		}

		// Token: 0x060051BA RID: 20922 RVA: 0x0021FDA2 File Offset: 0x0021DFA2
		public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
		{
			if (card.Owner.Creature != base.Owner || card.Type != CardType.Skill)
			{
				modifiedCost = originalCost;
				return false;
			}
			modifiedCost = 0m;
			return true;
		}

		// Token: 0x060051BB RID: 20923 RVA: 0x0021FDD1 File Offset: 0x0021DFD1
		[NullableContext(0)]
		public override ValueTuple<PileType, CardPilePosition> ModifyCardPlayResultPileTypeAndPosition([Nullable(1)] CardModel card, bool isAutoPlay, ResourceInfo resources, PileType pileType, CardPilePosition position)
		{
			if (card.Owner.Creature != base.Owner)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			if (card.Type != CardType.Skill)
			{
				return new ValueTuple<PileType, CardPilePosition>(pileType, position);
			}
			return new ValueTuple<PileType, CardPilePosition>(PileType.Exhaust, position);
		}
	}
}
