using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;

namespace MegaCrit.Sts2.Core.Models.Relics
{
	// Token: 0x0200057C RID: 1404
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class RingingTriangle : RelicModel
	{
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x00219FEB File Offset: 0x002181EB
		public override RelicRarity Rarity
		{
			get
			{
				return RelicRarity.Shop;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x00219FEE File Offset: 0x002181EE
		protected override IEnumerable<IHoverTip> ExtraHoverTips
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromKeyword(CardKeyword.Retain));
			}
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x00219FFB File Offset: 0x002181FB
		public override bool ShouldFlush(Player player)
		{
			return player != base.Owner || player.Creature.CombatState.RoundNumber > 1;
		}
	}
}
