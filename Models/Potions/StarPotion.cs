using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x02000719 RID: 1817
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class StarPotion : PotionModel
	{
		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x06005888 RID: 22664 RVA: 0x0022B3E3 File Offset: 0x002295E3
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Common;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06005889 RID: 22665 RVA: 0x0022B3E6 File Offset: 0x002295E6
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x0600588A RID: 22666 RVA: 0x0022B3E9 File Offset: 0x002295E9
		public override TargetType TargetType
		{
			get
			{
				return TargetType.Self;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x0600588B RID: 22667 RVA: 0x0022B3EC File Offset: 0x002295EC
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new StarsVar(3));
			}
		}

		// Token: 0x0600588C RID: 22668 RVA: 0x0022B3FC File Offset: 0x002295FC
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			await PlayerCmd.GainStars(base.DynamicVars.Stars.BaseValue, base.Owner);
		}
	}
}
