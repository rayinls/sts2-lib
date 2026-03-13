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
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Potions
{
	// Token: 0x0200070E RID: 1806
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class PowderedDemise : PotionModel
	{
		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x0600583E RID: 22590 RVA: 0x0022AE1B File Offset: 0x0022901B
		public override PotionRarity Rarity
		{
			get
			{
				return PotionRarity.Uncommon;
			}
		}

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x0022AE1E File Offset: 0x0022901E
		public override PotionUsage Usage
		{
			get
			{
				return PotionUsage.CombatOnly;
			}
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06005840 RID: 22592 RVA: 0x0022AE21 File Offset: 0x00229021
		public override TargetType TargetType
		{
			get
			{
				return TargetType.AnyEnemy;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06005841 RID: 22593 RVA: 0x0022AE24 File Offset: 0x00229024
		protected override IEnumerable<DynamicVar> CanonicalVars
		{
			get
			{
				return new <>z__ReadOnlySingleElementList<DynamicVar>(new DynamicVar("Demise", 9m));
			}
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x0022AE3C File Offset: 0x0022903C
		protected override async Task OnUse(PlayerChoiceContext choiceContext, [Nullable(2)] Creature target)
		{
			PotionModel.AssertValidForTargetedPotion(target);
			await PowerCmd.Apply<DemisePower>(target, base.DynamicVars["Demise"].BaseValue, base.Owner.Creature, null, false);
		}

		// Token: 0x04002280 RID: 8832
		private const string _demiseKey = "Demise";
	}
}
