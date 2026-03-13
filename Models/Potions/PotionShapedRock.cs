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
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x0200070C RID: 1804
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PotionShapedRock : PotionModel
	{
		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06005831 RID: 22577 RVA: 0x0022AD33 File Offset: 0x00228F33
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Token;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06005832 RID: 22578 RVA: 0x0022AD36 File Offset: 0x00228F36
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06005833 RID: 22579 RVA: 0x0022AD39 File Offset: 0x00228F39
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06005834 RID: 22580 RVA: 0x0022AD3C File Offset: 0x00228F3C
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DamageVar(15m, ValueProp.Unpowered));
			}
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x0022AD50 File Offset: 0x00228F50
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await CreatureCmd.Damage(choiceContext, target, base.DynamicVars.Damage, base.Owner.Creature, null);
		}
	}
}
